using System;
using Game.System;
using JetBrains.Annotations;
using RedBjorn.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.System
{
    public interface IMoveAction
    {
        public void PlayerMoveTo(GameObject player, Vector2 path);
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
        public void PlayerMoveTo(GameObject player, Vector2 path)
        {
            // Debug.LogWarning(GameBody.GetSystem<MapSystem>().GetRoundHexCell(player.GetComponent<Player>().CurHexCell.Pos ,2).Length);
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
            player.GetComponent<Rigidbody2D>().MovePosition(newCell.transform.position);
            player.GetComponent<Player>().CurHexCell = newCell;
            if (newCell.Type == HexType.Transport)
            {
                //TODO::������ϷΪͨ��
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
            Debug.LogWarning(player.GetComponent<Player>().MoveTimes.ToString());
            player.GetComponent<Player>().MoveTimes.AddValue(-1);
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
                if (cellUnit.OccupyObject.GetComponent<BaseEntity>().bMisLead)
                {
                    ThrowTarget(enemy, cellUnit);
                    break;
                }
                else if (cellUnit.OccupyObject.GetComponent<BaseEntity>().IsPlayer)
                {
                    ThrowTarget(enemy, cellUnit);
                    break;
                }
            }

            // û��⵽��һ��߳�޶������·��
            Vector2 target = GameBody.GetSystem<MapSystem>().RandomPatrol(enemy.GetComponent<Enemy>().SpawnPoint,
                enemy.GetComponent<Enemy>().CurHexCell.Pos);
            ThrowTarget(enemy, GridManager.Instance.hexCells[(int) target.x, (int) target.y]);
        }

        private void ThrowTarget(GameObject enemy, HexCell target)
        {
            // �ж���Ϊ0���˳�
            if (enemy.GetComponent<Enemy>().MoveTimes <= 0 || enemy.GetComponent<Enemy>().StepLength <= 0)
            {
                return;
            }

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            List<HexCell> WholePath = GameBody.GetSystem<MapSystem>()
                .GetPath(enemy.GetComponent<Enemy>().CurHexCell.Pos, target.Pos);
            WholePath.RemoveAt(WholePath.Count - 1);
            HexCell lastCell = null;
            foreach (var cell in WholePath)
            {
                rb.MovePosition(cell.transform.position);
                enemy.GetComponent<Enemy>().CurHexCell = cell;
                if (cell.Type == HexType.Thorns || cell.Type == HexType.Moon || cell.Type == HexType.Fire)
                {
                    StateSystem.Execution(new List<string>(cell.Instructions), enemy);
                }

                if (string.IsNullOrEmpty(enemy.GetComponent<Enemy>().SpawningPath))
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

            // ����ͣ����
            if (!new List<HexType>() {HexType.Spar, HexType.Spore}.Contains(enemy.GetComponent<Enemy>().CurHexCell
                .Type))
            {
                StateSystem.Execution(new List<string>(enemy.GetComponent<Enemy>().CurHexCell.Instructions), enemy);
            }
        }

        [Obsolete]
        // ���ݵ�λ�ĵ�ǰλ�á�Ŀ�ĵ�λ�ú��ƶ����������ƶ��Ĵ������ƶ���λ
        public void Move(Vector2 position, Vector2 targetPosition, int stepLength, GameObject unit)
        {
            Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
            if (unit.CompareTag("Player"))
            {
                float distance = Vector2.Distance(position, targetPosition);
                if (distance <= stepLength)
                {
                    // ʹ�� Rigidbody ���������ƶ�
                    rb.MovePosition(targetPosition);
                }
            }
            else if (unit.CompareTag("Monster"))
            {
                Run_AI(position, stepLength, unit);
            }
        }

        [Obsolete]
        // ��������AI�ƶ�����
        void Run_AI(Vector2 position, int stepLength, GameObject unit)
        {
            var hexCells = mapSystem.GetRoundHexCell(position, stepLength);
            Vector2? targetPos = null;
            foreach (var cell in hexCells)
            {
                if (cell)
                {
                    targetPos = cell.Pos;
                    break;
                }
            }

            if (targetPos.HasValue)
            {
                Run_Move(position, targetPos.Value, stepLength, unit);
            }
            else
            {
                RandomMove(position, stepLength, unit);
            }
        }

        [Obsolete]
        // ִ������ƶ�
        void RandomMove(Vector2 position, int stepLength, GameObject unit)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized * stepLength;
            Vector2 newPosition = position + randomDirection;
            Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
            rb.MovePosition(newPosition);
        }

        [Obsolete]
        // �ݹ��ȡ�ӵ�ǰλ�õ�Ŀ��λ�õ����·��
        void Run_Move(Vector2 currentPosition, Vector2 targetPosition, int stepLength, GameObject unit)
        {
            List<Vector2> path = null;
            if (path != null && path.Count > 0)
            {
                Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
                // �ƶ���·������һ����
                rb.MovePosition(path[0]);
            }
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
            PlayerMoveTo(Player.instance.gameObject, Player.instance.LastHexCell.Pos);
        }
    }
}