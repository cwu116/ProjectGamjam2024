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
        public void Move(GameObject gameObject, Vector3 target)//���ƶ���
        {
            int Move;//�ƶ���
            int[,] a;//���ﵱǰ����
            int[,] n;//Ŀ������
            int[][,] c;//λ��·��(λ��·��)
            if (gameObject.tag.Equals("Player"))//����ƶ��߼�
            {

                return;
            }
            else if (gameObject.tag.Equals("Enemy"))//�����ƶ�AI
            {
                return;
            }
        }
        void Run_AI(GameObject gameObject)
        {
            MapSystem mapSystem = new MapSystem();
        }
        public void Run_Move(Vector2 Xy, int Move)//��ȡ���·�������
        {

        }
        public override void InitSystem()
        {

        }
    }
}

