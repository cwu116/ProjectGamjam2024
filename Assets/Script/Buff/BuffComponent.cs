using System.Collections.Generic;
using UnityEngine;

namespace Buff
{
    public class BuffComponent
    {
        public Dictionary<string, ValueInt> ValueUnits;         // 玩家数值类
        public Dictionary<State, int> StateUnits;               // 玩家状态类
        
        public BuffComponent(GameObject entity)
        {
            ValueUnits = new Dictionary<string, ValueInt>();
            StateUnits = new Dictionary<State, int>();
            ValueUnits.Add("Hp", new ValueInt(3));
            ValueUnits.Add("MaxHp", new ValueInt(3));
            ValueUnits.Add("MoveRange", new ValueInt(2));
            ValueUnits.Add("MoveForce", new ValueInt(1));
        }

        public void UseBuffGameStart()
        {
            
        }

        public void AddState(State state, int flow)
        {
            StateUnits.Add(state, flow);
        }

        public void RemoveState(State state)
        {
            StateUnits.Remove(state);
        }
    }

    // 赋值整数
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