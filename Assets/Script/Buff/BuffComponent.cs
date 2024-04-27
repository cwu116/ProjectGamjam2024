using System.Collections.Generic;
using Game.System;
using UnityEngine;

namespace Buff
{
    public class BuffComponent : MonoBehaviour
    {
        public Dictionary<string, ValueInt> ValueUnits;         // 玩家数值类
        public Dictionary<State, int> StateUnits;               // 玩家状态类
        public bool isPlayer;
        
        public BuffComponent(GameObject entity)
        {
            ValueUnits = new Dictionary<string, ValueInt>();
            StateUnits = new Dictionary<State, int>();
            ValueUnits.Add("Hp", new ValueInt(3));
            ValueUnits.Add("MaxHp", new ValueInt(3));
            ValueUnits.Add("MoveRange", new ValueInt(2));
            ValueUnits.Add("MoveForce", new ValueInt(1));
        }
        
        public void AddState(State state, int flow)
        {
            StateUnits.Add(state, flow);
        }

        public void RemoveState(State state)
        {
            StateUnits.Remove(state);
        }

        public void ClearState()
        {
            StateUnits.Clear();
            foreach (var unit in ValueUnits)
            {
                ValueUnits[unit.Key].RemoveChange();
            }
        }

        public void StatesStart()
        {
            foreach (var unit in StateUnits)
            {
                if (unit.Key.isStartExec)
                {
                    if (unit.Key.isAdditive)
                    {
                        for (int i = 0; i < unit.Value; i++)
                        {
                            StateSystem.Execution(unit.Key.buffCMD, transform.parent.gameObject);   
                        }
                    }
                    else
                    {
                        StateSystem.Execution(unit.Key.buffCMD, transform.parent.gameObject);
                    }
                    StateUnits[unit.Key] -= 1;
                    if (StateUnits[unit.Key] < 0)
                    {
                        RemoveState(unit.Key);
                    }
                }
            }
        }

        public void StatesEnd()
        {
            foreach (var unit in StateUnits)
            {
                if (!unit.Key.isStartExec)
                {
                    if (unit.Key.isAdditive)
                    {
                        for (int i = 0; i < unit.Value; i++)
                        {
                            StateSystem.Execution(unit.Key.buffCMD, transform.parent.gameObject);   
                        }
                    }
                    else
                    {
                        StateSystem.Execution(unit.Key.buffCMD, transform.parent.gameObject);
                    }
                    StateUnits[unit.Key] -= 1;
                }
                if (StateUnits[unit.Key] < 0)
                {
                    RemoveState(unit.Key);
                }
            }
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
        
        public void AddValue(int Value, bool isChange = false)
        {
            if (!isChange)
            {
                changeValue += Value;
            }
            else
            {
                changeValue = Value;
            }
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