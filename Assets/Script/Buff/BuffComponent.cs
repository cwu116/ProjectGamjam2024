using System;
using System.Collections.Generic;
using Buff.Tool;
using Game.System;
using UnityEngine;

namespace Buff
{
    interface IBuffComponent
    {
        public void RegisterParam(string paramName, ValueInt value){}
        public void RegisterFunc(string funcName, BuffComponent.Custom func){}
        public void RegisterFunc(string funcName, BuffComponent.TCustom func){}
        
    }
    public class BuffComponent : MonoBehaviour
    {
        public delegate void Custom();
        public delegate void TCustom(params Param[] param);

        public Dictionary<string, ValueInt> ValueUnits;         // 玩家数值类
        public Dictionary<State, int> StateUnits;               // 玩家状态类
        public Dictionary<string, Custom> FuncUnits;            // 玩家方法类
        public Dictionary<string, TCustom> TFuncUnits;          // 有参玩家方法类
        public bool isPlayer;

        private void Awake()
        {
            ValueUnits = new Dictionary<string, ValueInt>();
            StateUnits = new Dictionary<State, int>();
        }

        public ValueInt Get(string paramName)
        {
            return ValueUnits[paramName];
        }

        private void TestFunc()
        {
            Debug.LogWarning("Func called");
        }

        public void RegisterParam(string paramName, ValueInt value)
        {
            ValueUnits.Add(paramName, value);
        }

        public void RegisterFunc(string funcName, Custom func)
        {
            FuncUnits.Add(funcName, func);
        }

        public void RegisterFunc(string funcName, TCustom func)
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