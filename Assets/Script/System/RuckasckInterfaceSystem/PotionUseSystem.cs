using Buff;
using Buff.Config;
using Game.Model;
using Game.System;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.System
{
    public class PotionUseSystem : BaseSystem
    {
        public void Use(Item_Data item_Data, GameObject EffectObject)//ҩˮ,��Ч����
        {

            StateSystem.Execution(item_Data.Type, EffectObject);
            Debug.Log("��Ч");
        }
        public override void InitSystem()
        {
        }
    }
}
