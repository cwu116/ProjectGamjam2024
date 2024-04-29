using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    public class EnemyModel : BaseModel
    {
        public string name;            //��������
        public int health;             //Ѫ��
        public int actionCount;        //�ж���
        public int movingRange;        //�ƶ���
        public int vigilanceRange;     //������Χ
        public string skillDescription;//��������
        public int actionCost;         //�����ж���
        public char dropMaterial;      //���ϵ���

        public override void InitModel()
        {
            //TODO:��ʼ��Model
        }
    }
}

