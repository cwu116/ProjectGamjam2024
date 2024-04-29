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
        string[] Ts = {  };//特殊效果处理列表
        public void Use(Item_Data item_Data, GameObject EffectObject)//药水,生效对象
        {
            foreach (var t in Ts)
            {
                if (item_Data.Name.Equals(t))
                {
                    Debug.Log("生效");
                    return;
                }
            }
            StateSystem.Execution(item_Data.Type, EffectObject);
            Debug.Log("生效");
        }
        public override void InitSystem()
        {

        }
    }/*
    public class EffectObject
    {
        public int MaxHp;//最大生命值
        public int Attck;//攻击力
        public int StepLenghth;//移动力
        public int RangeLeft;//范围开始
        public int RangeRight;//范围结束
        public int MaxMoveTimes;//行动点上限
        public int Defence;//防御力
    }*/
}
