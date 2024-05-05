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
        public void Use(Item_Data item_Data, GameObject EffectObject)//药水,生效对象
        {
            EventSystem.Send<UsePotionEvent>(new UsePotionEvent() { potion = item_Data });
            StateSystem.Execution(item_Data.Type, EffectObject);
            Debug.Log("生效"+EffectObject.name+" "+item_Data.Name);
        }
        public override void InitSystem()
        {
        }
    }
}
