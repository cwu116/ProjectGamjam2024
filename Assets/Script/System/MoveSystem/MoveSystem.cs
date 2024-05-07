using System;
using Game.System;
using JetBrains.Annotations;
using RedBjorn.Utils;
using System.Collections;
using System.Collections.Generic;
using Buff.Tool;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using System.Threading.Tasks;

namespace Game.System
{
    public interface IMoveAction
    {
        public void PlayerMoveTo(GameObject player, Vector2 path, bool isUndo = false);
        public void EnemyMoveTo(GameObject enemy);
    }

    public class MoveSystem : BaseSystem, IMoveAction, ICanUndo
    {
        private MapSystem mapSystem = new MapSystem();

        /// <summary>
        /// ����ƶ���Ŀ�꣨ȫ·����
        /// </summary>
        /// <param name="player">���</param>
        /// <param name="path">·��</param>
        public void PlayerMoveTo(GameObject player, Vector2 path, bool isUndo = false)
        {
            if (GameBody.GetSystem<MapSystem>().CalculateDistance(player.GetComponent<Player>().CurHexCell.Pos,
                path) > 1 || GameBody.GetSystem<MapSystem>().CalculateDistance(
                player.GetComponent<Player>().CurHexCell.Pos,
                path) == 0)
            {
                return;
            }

            if (player.GetComponent<Player>().MoveTimes <= 0)
            {
                return;
            }

            HexCell newCell = GridManager.Instance.hexCells[(int) path.x, (int) path.y];
            if (newCell.OccupyObject is not null && !newCell.OccupyObject.GetComponent<Herb>() )
            {
                return;
            }

            if (newCell == player.GetComponent<Player>().CurHexCell)
            {
                return;
            }

            player.GetComponent<Player>().LastHexCell = player.GetComponent<Player>().CurHexCell;
            player.GetComponent<Player>().LastHexCell.OccupyObject = null;
            player.GetComponent<Player>().CurHexCell = newCell;
            player.GetComponent<Player>().CurHexCell.OccupyObject = player;
            if (newCell.Type == HexType.Transport)
            {
                EventSystem.Send<GameSuccessEvent>();
                //TODO::通关事件
            }

            if (!string.IsNullOrEmpty(player.GetComponent<Player>().SpawningPath))
            {
                string[] spawnInfo = player.GetComponent<Player>().SpawningPath.Split(new[] {'*'});
                StateSystem.Execution(new List<string>()
                {
                    string.Format("Delay:[Create:true,{0},this],1", spawnInfo[0]),
                    string.Format("Delay:[Create:true,{0},this],{1}", newCell.Type.ToString(), 1 + spawnInfo[1])
                }, newCell.gameObject);
                player.GetComponent<Player>().SpawningPath = null;
            }

            // Debug.LogWarning("Buff:" + string.Join(' ', newCell.Instructions));
            StateSystem.Execution(new List<string>(newCell.Instructions), player);
            player.GetComponent<Player>().MoveTimes += -1;
            EventSystem.Send(new PlayerMoveEvent()
                {currentCell = newCell, moveTimes = player.GetComponent<Player>().MoveTimes});
            //if(!isUndo)
            //EventSystem.Send(new UndoEvent() {undoOperation=this });
        }

        /// <summary>
        /// ��������ĳλ��
        /// </summary>
        /// <param name="enemy">����</param>
        public void EnemyMoveTo(GameObject enemy)
        {
            List<HexCell> hexcells = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                .GetRoundHexCell(enemy.GetComponent<Enemy>().CurHexCell.Pos, enemy.GetComponent<Enemy>().WatchRange));
            foreach (var cellUnit in hexcells)
            {
                if (cellUnit.OccupyObject is null || cellUnit.OccupyObject == enemy)
                {
                    continue;
                }

                if (cellUnit.OccupyObject.GetComponent<BaseEntity>().bMisLead ||
                    cellUnit.OccupyObject.GetComponent<BaseEntity>().IsPlayer &&
                    !cellUnit.OccupyObject.GetComponent<BaseEntity>().bInvisible)
                {
                    if (enemy.GetComponent<Enemy>().RangeRight < 1)
                    {
                        break;
                    }

                    enemy.GetComponent<Enemy>().isDisturbed = true;
                    Debug.LogError("I see you!");
                    ThrowTarget(enemy, cellUnit);
                    return;
                }
            }

            Vector2 target = Vector2.zero;
            HexCell cell = GridManager.Instance.hexCells[(int) target.x, (int) target.y];
            int times = 0;
            do
            {
                target = GameBody.GetSystem<MapSystem>().RandomPatrol(enemy.GetComponent<Enemy>().SpawnPoint,
                    enemy.GetComponent<Enemy>().CurHexCell.Pos);
                times++;
            } while ((cell.Type == HexType.Thorns || cell.Type == HexType.Moon || cell.Type == HexType.Fire) &&
                     times < 30);

            enemy.GetComponent<Enemy>().isDisturbed = false;
            ThrowTarget(enemy, GridManager.Instance.hexCells[(int) target.x, (int) target.y]);
        }

        private async void ThrowTarget(GameObject enemy, HexCell target)
        {
                if (enemy.GetComponent<Enemy>().MoveTimes <= 0 || enemy.GetComponent<Enemy>().StepLength <= 0)
                {
                    Debug.LogError("Last:" + enemy.GetComponent<Enemy>().MoveTimes);
                    return;
                }

                List<HexCell> WholePath = GameBody.GetSystem<MapSystem>()
                    .GetPath(enemy.GetComponent<Enemy>().CurHexCell.Pos, target.Pos);

                foreach (var cell in WholePath)//先判断攻击
                {
                    if (cell.OccupyObject != null && (cell.OccupyObject.GetComponent<BaseEntity>().IsPlayer ||
                                                      cell.OccupyObject.GetComponent<BaseEntity>().bMisLead))
                    {
                        if ((GameBody.GetSystem<MapSystem>()
                                .CalculateDistance(enemy.GetComponent<Enemy>().CurHexCell.Pos, cell.Pos)) <=
                            enemy.GetComponent<Enemy>().RangeRight && enemy.GetComponent<Enemy>().MoveTimes > 0) //在攻击范围内
                        {
                            //使用技能
                            if (cell.OccupyObject.GetComponent<BaseEntity>().IsPlayer)
                                enemy.GetComponent<Enemy>()
                                    .UseSkill(cell.OccupyObject.GetComponent<BaseEntity>() as Player);
                            else
                                enemy.GetComponent<Enemy>().UseSkill(cell.OccupyObject.GetComponent<BaseEntity>());
                        }

                        break;
                    }
                }

                foreach (var cell in WholePath)//再执行移动或攻击
                {
                    if (cell.OccupyObject != null && (cell.OccupyObject.GetComponent<BaseEntity>().IsPlayer ||
                                                      cell.OccupyObject.GetComponent<BaseEntity>().bMisLead))
                    {
                        if ((GameBody.GetSystem<MapSystem>()
                                .CalculateDistance(enemy.GetComponent<Enemy>().CurHexCell.Pos, cell.Pos)) <=
                            enemy.GetComponent<Enemy>().RangeRight && enemy.GetComponent<Enemy>().MoveTimes > 0) //在攻击范围内并且有行动力
                        {
                            //使用技能
                            if (cell.OccupyObject.GetComponent<BaseEntity>().IsPlayer)
                                enemy.GetComponent<Enemy>()
                                    .UseSkill(cell.OccupyObject.GetComponent<BaseEntity>() as Player);
                            else
                                enemy.GetComponent<Enemy>().UseSkill(cell.OccupyObject.GetComponent<BaseEntity>());
                        }

                        break;
                    }
                    else if (cell.OccupyObject != null && cell.OccupyObject.GetComponent<BaseEntity>() is Enemy)
                    {
                        continue;
                    }

                    if (enemy.GetComponent<Enemy>().MoveTimes <= 0)
                        return;
                    enemy.GetComponent<Enemy>().anim.SetTrigger("Move");
                    await Task.Delay(300);
                    enemy.GetComponent<Enemy>().LastHexCell = enemy.GetComponent<Enemy>().CurHexCell;
                    enemy.GetComponent<Enemy>().LastHexCell.OccupyObject = null;
                    enemy.GetComponent<Enemy>().CurHexCell = cell;
                    enemy.GetComponent<Enemy>().CurHexCell.OccupyObject = enemy;
                    enemy.transform.DOMove(cell.transform.position, 0.5f);
                    enemy.GetComponent<Enemy>().CurHexCell = cell;
                    if (cell.Type == HexType.Thorns || cell.Type == HexType.Moon || cell.Type == HexType.Fire)
                    {
                        StateSystem.Execution(new List<string>(cell.Instructions), enemy);
                    }

                    if (!string.IsNullOrEmpty(enemy.GetComponent<Enemy>().SpawningPath))
                    {
                        string[] spawnInfo = enemy.GetComponent<Enemy>().SpawningPath.Split(new[] { '*' });
                        StateSystem.Execution(new List<string>()
                    {
                        string.Format("Delay:[Create:{0},this],1", spawnInfo[0]),
                        string.Format("Delay:[Create:{0},this],{1}", cell.Type.ToString(), 1 + spawnInfo[1])
                    }, cell.gameObject);

                        enemy.GetComponent<Enemy>().SpawningPath = null;
                    }

                    enemy.GetComponent<Enemy>().MoveTimes.AddValue(-1);
                    if (enemy.GetComponent<Enemy>().MoveTimes <= 0)
                    {
                        break;
                    }
            }

            EventSystem.Send<EnemyActionComplete>(new EnemyActionComplete() { enemy = enemy.GetComponent<Enemy>() });
        }

        public override void InitSystem()
        {
            // ϵͳ��ʼ���߼�
            RegisterEvents();
        }

        void RegisterEvents()
        {
            EventSystem.Register<GirdCilckEvent>(v => { PlayerMoveTo(Player.instance.gameObject, v.cell.Pos); });
        }

        public void Undo()
        {
            PlayerMoveTo(Player.instance.gameObject, Player.instance.LastHexCell.Pos, true);
            Player.instance.MoveTimes.AddValue(1);
            Debug.Log("+1");
        }
    }
}