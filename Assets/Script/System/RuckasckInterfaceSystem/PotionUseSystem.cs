using Buff;
using Buff.Config;
using Game.Model;
using Game.System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.System
{
    public class PotionUseSystem : BaseSystem
    {
        string[] Ts = {  };//����Ч�������б�
        public void Use(Item_Data item_Data, GameObject EffectObject)//ҩˮ,��Ч����
        {
            foreach (var t in Ts)
            {
                if (item_Data.Name.Equals(t))
                {
                    Debug.Log("��Ч");
                    return;
                }
            }
            StateSystem.Execution(item_Data.Type, EffectObject);
            Debug.Log("��Ч");
        }
        public override void InitSystem()
        {

        }
    }/*
    public class EffectObject
    {
        public int MaxHp;//�������ֵ
        public int Attck;//������
        public int StepLenghth;//�ƶ���
        public int RangeLeft;//��Χ��ʼ
        public int RangeRight;//��Χ����
        public int MaxMoveTimes;//�ж�������
        public int Defence;//������
    }*/
}
