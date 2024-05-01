using Game.System;
using JetBrains.Annotations;
using RedBjorn.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.System
{
    public class MoveSystem : BaseSystem
    {
        private MapSystem mapSystem = new MapSystem();

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

        // 执行随机移动
        void RandomMove(Vector2 position, int stepLength, GameObject unit)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized * stepLength;
            Vector2 newPosition = position + randomDirection;
            Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
            rb.MovePosition(newPosition);
        }

        // 递归获取从当前位置到目标位置的最短路径
        void Run_Move(Vector2 currentPosition, Vector2 targetPosition, int stepLength, GameObject unit)
        {
            List<Vector2> path =null;
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
        }
    }
}
