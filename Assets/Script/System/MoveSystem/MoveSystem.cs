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

        // ִ������ƶ�
        void RandomMove(Vector2 position, int stepLength, GameObject unit)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized * stepLength;
            Vector2 newPosition = position + randomDirection;
            Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
            rb.MovePosition(newPosition);
        }

        // �ݹ��ȡ�ӵ�ǰλ�õ�Ŀ��λ�õ����·��
        void Run_Move(Vector2 currentPosition, Vector2 targetPosition, int stepLength, GameObject unit)
        {
            List<Vector2> path =null;
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
