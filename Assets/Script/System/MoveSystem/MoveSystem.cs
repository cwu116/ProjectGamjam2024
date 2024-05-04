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
        /// 玩家移动至目标（全路径）
        /// </summary>
        /// <param name="player">玩家</param>
        /// <param name="path">路径</param>
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
                //TODO::设置游戏为通关
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
        /// 敌人移至某位置
        /// </summary>
        /// <param name="enemy">敌人</param>
        public void EnemyMoveTo(GameObject enemy)
        {
            // 寻找警戒范围内目标
            List<HexCell> hexcells = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                .GetRoundHexCell(enemy.GetComponent<Enemy>().CurHexCell.Pos, enemy.GetComponent<Enemy>().WatchRange));
            // 寻找可移动最大范围目标
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

            // 没检测到玩家或者仇恨对象，随机路径
            Vector2 target = GameBody.GetSystem<MapSystem>().RandomPatrol(enemy.GetComponent<Enemy>().SpawnPoint,
                enemy.GetComponent<Enemy>().CurHexCell.Pos);
            ThrowTarget(enemy, GridManager.Instance.hexCells[(int) target.x, (int) target.y]);
        }

        private void ThrowTarget(GameObject enemy, HexCell target)
        {
            // 行动点为0，退出
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

            // 敌人停留点
            if (!new List<HexType>() {HexType.Spar, HexType.Spore}.Contains(enemy.GetComponent<Enemy>().CurHexCell
                .Type))
            {
                StateSystem.Execution(new List<string>(enemy.GetComponent<Enemy>().CurHexCell.Instructions), enemy);
            }
        }

        [Obsolete]
        // 根据单位的当前位置、目的地位置和移动力（可以移动的次数）移动单位
        public void Move(Vector2 position, Vector2 targetPosition, int stepLength, GameObject unit)
        {
            Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
            if (unit.CompareTag("Player"))
            {
                float distance = Vector2.Distance(position, targetPosition);
                if (distance <= stepLength)
                {
                    // 使用 Rigidbody 进行物理移动
                    rb.MovePosition(targetPosition);
                }
            }
            else if (unit.CompareTag("Monster"))
            {
                Run_AI(position, stepLength, unit);
            }
        }

        [Obsolete]
        // 处理怪物的AI移动决策
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
        // 执行随机移动
        void RandomMove(Vector2 position, int stepLength, GameObject unit)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized * stepLength;
            Vector2 newPosition = position + randomDirection;
            Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
            rb.MovePosition(newPosition);
        }

        [Obsolete]
        // 递归获取从当前位置到目标位置的最短路径
        void Run_Move(Vector2 currentPosition, Vector2 targetPosition, int stepLength, GameObject unit)
        {
            List<Vector2> path = null;
            if (path != null && path.Count > 0)
            {
                Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
                // 移动到路径的下一个点
                rb.MovePosition(path[0]);
            }
        }

        public override void InitSystem()
        {
            // 系统初始化逻辑
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