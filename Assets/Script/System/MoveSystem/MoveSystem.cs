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
                path) > 1)
            {
                return;
            }

            if (player.GetComponent<Player>().MoveTimes <= 0)
            {
                return;
            }
            
            player.GetComponent<Player>().LastHexCell = player.GetComponent<Player>().CurHexCell;

            HexCell newCell = GridManager.Instance.hexCells[(int) path.x, (int) path.y];
            player.GetComponent<Player>().LastHexCell.OccupyObject = null;
            player.GetComponent<Player>().CurHexCell = newCell;
            if (newCell.Type == HexType.Transport)
            {
                //TODO::通关事件
            }

            if (!string.IsNullOrEmpty(player.GetComponent<Player>().SpawningPath))
            {
                string[] spawnInfo = player.GetComponent<Player>().SpawningPath.Split(new[] {'*'});
                StateSystem.Execution(new List<string>()
                {
                    string.Format("Delay:[Create:{0},this],1", spawnInfo[0]),
                    string.Format("Delay:[Create:{0},this],{1}", newCell.Type.ToString(), 1 + spawnInfo[1])
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
            // Ѱ�Ҿ��䷶Χ��Ŀ��
            List<HexCell> hexcells = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                .GetRoundHexCell(enemy.GetComponent<Enemy>().CurHexCell.Pos, enemy.GetComponent<Enemy>().WatchRange));
            // Ѱ�ҿ��ƶ����ΧĿ��
            List<HexCell> AllCell = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                .GetRoundHexCell(enemy.GetComponent<Enemy>().CurHexCell.Pos,
                    enemy.GetComponent<Enemy>().MoveTimes * enemy.GetComponent<Enemy>().StepLength));
            foreach (var cellUnit in hexcells)
            {
                if (cellUnit.OccupyObject is null || cellUnit.OccupyObject == enemy)
                {
                    continue;
                }
                if (cellUnit.OccupyObject.GetComponent<BaseEntity>().bMisLead)
                {
                    Debug.LogError("I see you!");
                    ThrowTarget(enemy, cellUnit);
                    return;
                }
                else if (cellUnit.OccupyObject.GetComponent<BaseEntity>().IsPlayer)
                {
                    Debug.LogError("I see you!");
                    ThrowTarget(enemy, cellUnit);
                    return;
                }
            }

            // û��⵽��һ��߳�޶������·��
            Vector2 target = GameBody.GetSystem<MapSystem>().RandomPatrol(enemy.GetComponent<Enemy>().SpawnPoint,
                enemy.GetComponent<Enemy>().CurHexCell.Pos);
            ThrowTarget(enemy, GridManager.Instance.hexCells[(int) target.x, (int) target.y]);
            
           
        }

        private async void ThrowTarget(GameObject enemy, HexCell target)
        {
            if (enemy.GetComponent<Enemy>().MoveTimes <= 0 || enemy.GetComponent<Enemy>().StepLength <= 0)
            {
                return;
            }
            
            List<HexCell> WholePath = GameBody.GetSystem<MapSystem>()
                .GetPath(enemy.GetComponent<Enemy>().CurHexCell.Pos, target.Pos);
            HexCell lastCell = null;
            foreach (var cell in WholePath)
            {
                enemy.GetComponent<Enemy>().anim.SetTrigger("Move");
                await Task.Delay(300);
                enemy.transform.DOMove(cell.transform.position, 0.5f);
                enemy.GetComponent<Enemy>().CurHexCell = cell;
                if (cell.Type == HexType.Thorns || cell.Type == HexType.Moon || cell.Type == HexType.Fire)
                {
                    StateSystem.Execution(new List<string>(cell.Instructions), enemy);
                }

                if (!string.IsNullOrEmpty(enemy.GetComponent<Enemy>().SpawningPath))
                {
                    string[] spawnInfo = enemy.GetComponent<Enemy>().SpawningPath.Split(new[] {'*'});
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