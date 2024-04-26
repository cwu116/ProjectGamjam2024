using System.Collections.Generic;
using UnityEngine;

namespace Buff
{
    public class BuffComponent
    {
        public Dictionary<string, ValueInt> ValueUnits;         // 玩家数值类
        public List<State> States;                              // 玩家状态类
        
        public BuffComponent(GameObject entity)
        {
            ValueUnits = new Dictionary<string, ValueInt>();
            States = new List<State>();
            ValueUnits.Add("Hp", new ValueInt(3));
            ValueUnits.Add("MaxHp", new ValueInt(3));
            ValueUnits.Add("MoveRange", new ValueInt(2));
            ValueUnits.Add("MoveForce", new ValueInt(1));
        }

        public void UseBuffGameStart()
        {
            
        }

        public void AddState(State state)
        {
            States.Add(state);
        }

        public void RemoveState(State state)
        {
            States.Remove(state);
        }
    }

    // 
    public class ValueInt
    {
        private int baseValue;
        private int changeValue;
        

        public ValueInt(int baseValue)
        {
            this.baseValue = baseValue;
        }
        
        public void AddValue(int change)
        {
            changeValue = change;
        }

        public void RemoveChange()
        {
            changeValue = 0;
        }

        public static implicit operator int(ValueInt unit)
        {
            return unit.baseValue + unit.changeValue;
        }

        public override string ToString()
        {
            return (baseValue + changeValue).ToString();
        }
        
    }
}