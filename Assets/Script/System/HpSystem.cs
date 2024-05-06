using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.GameBody;
using Game.Model;
using Buff;

namespace Game.System
{
    public class HpSystem : MonoBehaviour
    {
        private int _modelHp = 0;
        private AttackUnit_Data attackUnit_Data;

        HpUIEvent hpUIEvent = new HpUIEvent();
        AttackUnitModel attackUnitModel;

        void Update()
        {
            string name = this.transform.parent.name;
            attackUnit_Data = attackUnitModel.GetDataByName(name);
            if (_modelHp != attackUnit_Data.hp)
            {
                _modelHp = attackUnit_Data.hp;
                hpUIEvent.CurrentHp = attackUnit_Data.hp;
                EventSystem.Send<HpUIEvent>(hpUIEvent);
            }
        }

        
    }
}