using System;
using Game.System;
using JetBrains.Annotations;
using RedBjorn.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;
using Random = UnityEngine.Random;

namespace Game.System
{
    public interface IMoveAction
    {
        public void PlayerMoveTo(GameObject player, Vector2 target);
        public void EnemyMoveTo(GameObject enemy);
    }

    public class MoveSystem : BaseSystem, IMoveAction
    {
        private MapSystem mapSystem = new MapSystem();

        /// <summary>
        /// ����ƶ���Ŀ�꣨ȫ·����
        /// </summary>
        /// <param name="player">���</param>
        /// <param name="target">Ŀ�����</param>
        public void PlayerMoveTo(GameObject player, Vector2 target)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            List<HexCell> WholePath = GameBody.GetSystem<MapSystem>().GetPath(player.transform.position, target);
            foreach (var cell in WholePath)
            {
                // ����ƶ�
                rb.MovePosition(cell.Pos);
                if (cell.Type == HexType.Thorns || cell.Type == HexType.Moon)
                {
                    StateSystem.Execution(new List<string>(cell.Instructions), player);
                }

                if (cell.Type == HexType.Transport)
                {
                    //TODO::������ϷΪͨ��
                }

                if (string.IsNullOrEmpty(player.GetComponent<Player>().SpawningPath))
                {
                    string[] spawnInfo = player.GetComponent<Player>().SpawningPath.Split(new []{'*'});
                    StateSystem.Execution(new List<string>()
                    {
                        string.Format("Delay:[Create:{0},this],1", spawnInfo[0]),
                        string.Format("Delay:[Create:{0},this],{1}", cell.Type.ToString(), 1+spawnInfo[1])
                    }, cell.gameObject);
                    player.GetComponent<Player>().MoveTimes.AddValue(-1);
                    player.GetComponent<Player>().SpawningPath = null;
                }
            }

            // �趨������λ�ڵĸ���
            player.GetComponent<Player>().CurHexCell = WholePath[^1];
            // ���ͣ����
            if (!new List<HexType>() {HexType.Spar, HexType.Spore}.Contains(WholePath[^1].Type))
            {
                StateSystem.Execution(new List<string>(WholePath[^1].Instructions), player);
            }
        }

        /// <summary>
        /// ��������ĳλ��
        /// </summary>
        /// <param name="enemy">����</param>
        public void EnemyMoveTo(GameObject enemy)
        {
            // Ѱ�Ҿ��䷶Χ��Ŀ��
            List<HexCell> hexcells = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                .GetRoundHexCell(enemy.transform.position, enemy.GetComponent<Enemy>().WatchRange));
            // Ѱ�ҿ��ƶ����ΧĿ��
            List<HexCell> AllCell = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                .GetRoundHexCell(enemy.transform.position,
                    enemy.GetComponent<Enemy>().MoveTimes * enemy.GetComponent<Enemy>().StepLength));
            foreach (var cellUnit in hexcells)
            {
                if (cellUnit.OccupyObject.GetComponent<BaseEntity>().IsPlayer)
                {
                    ThrowTarget(enemy, cellUnit.OccupyObject);
                    break;
                }
            }

            // û��⵽��һ��߳�޶������·��
            HexCell target = null;
            // ��ȡ�ƶ��������������
            do
            {
                target = AllCell[Random.Range(0, AllCell.Count - 1)];
            } while (GameBody.GetSystem<MapSystem>().CalculateDistance(enemy.transform.position, target.Pos) %
                enemy.GetComponent<Enemy>().StepLength == 0);
            ThrowTarget(enemy, target.gameObject);
        }

        private void ThrowTarget(GameObject enemy, GameObject target)
        {
            // �ж���Ϊ0���˳�
            if (enemy.GetComponent<Enemy>().MoveTimes <= 0 || enemy.GetComponent<Enemy>().StepLength <= 0)
            {
                return;
            }

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            List<HexCell> WholePath = GameBody.GetSystem<MapSystem>()
                .GetPath(enemy.transform.position, target.transform.position);
            HexCell lastCell = null;
            foreach (var cell in WholePath)
            {
                rb.MovePosition(cell.Pos);
                if (cell.Type == HexType.Thorns || cell.Type == HexType.Moon)
                {
                    StateSystem.Execution(new List<string>(cell.Instructions), enemy);
                }
                if (string.IsNullOrEmpty(enemy.GetComponent<Enemy>().SpawningPath))
                {
                    string[] spawnInfo = enemy.GetComponent<Enemy>().SpawningPath.Split(new []{'*'});
                    StateSystem.Execution(new List<string>()
                    {
                        string.Format("Delay:[Create:{0},this],1", spawnInfo[0]),
                        string.Format("Delay:[Create:{0},this],{1}", cell.Type.ToString(), 1+spawnInfo[1])
                    }, cell.gameObject);
                    enemy.GetComponent<Enemy>().MoveTimes.AddValue(-1);
                    enemy.GetComponent<Enemy>().SpawningPath = null;
                }
            }

            // �趨�������λ�ڵĸ���
            enemy.GetComponent<Enemy>().CurHexCell = WholePath[^1];
            // ���ͣ����
            if (!new List<HexType>() {HexType.Spar, HexType.Spore}.Contains(WholePath[^1].Type))
            {
                StateSystem.Execution(new List<string>(WholePath[^1].Instructions), enemy);
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
        }
    }
}