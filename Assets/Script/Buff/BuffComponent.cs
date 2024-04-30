using System;
using System.Collections.Generic;
using Buff.Config;
using Buff.Tool;
using Game.System;
using UnityEngine;

namespace Buff
{
    public class BuffComponent : MonoBehaviour
    {
        public delegate void Custom();
        public delegate void TCustom(params Param[] param);

        public Dictionary<ValueKey, ValueInt> ValueUnits;       // 玩家数值类
        public Dictionary<State, int> StateUnits;               // 玩家状态类
        public Dictionary<ActionKey, Custom> FuncUnits;         // 玩家方法类
        public Dictionary<TActionKey, TCustom> TFuncUnits;      // 有参玩家方法类

        private void Awake()
        {
            ValueUnits = new Dictionary<ValueKey, ValueInt>();
            StateUnits = new Dictionary<State, int>();
        }

        public ValueInt Get(string paramName)
        {
            return ValueUnits[Enum.Parse<ValueKey>(paramName)];
        }

        public void RegisterParam(ValueKey paramName, ValueInt value)
        {
            ValueUnits.Add(paramName, value);
        }

        public void RegisterFunc(ActionKey funcName, Custom func)
        {
            FuncUnits.Add(funcName, func);
        }

        public void RegisterFunc(TActionKey funcName, TCustom func)
        {
            TFuncUnits.Add(funcName, func);
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


}