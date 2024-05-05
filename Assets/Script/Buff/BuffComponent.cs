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
        public List<StateUnit> StateUnits;                      // 玩家状态类
        public Dictionary<ActionKey, Custom> FuncUnits;         // 玩家方法类
        public Dictionary<TActionKey, TCustom> TFuncUnits;      // 有参玩家方法类

        private void Awake()
        {
            ValueUnits = new Dictionary<ValueKey, ValueInt>();
            StateUnits = new List<StateUnit>();
            FuncUnits = new Dictionary<ActionKey, Custom>();
            TFuncUnits = new Dictionary<TActionKey, TCustom>();
        }

        public ValueInt Get(string paramName)
        {
            return ValueUnits[Enum.Parse<ValueKey>(paramName)];
        }
        

        public void RegisterParam(ValueKey paramName, ValueInt value)
        {
            value.name = paramName.ToString();
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
        
        public void AddState(State state, int flow, GameObject target)
        {
            StateUnits.Add(new StateUnit(state, flow, target));
        }

        public void RemoveState(StateUnit state)
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
            for (int i = StateUnits.Count-1; i > 0; i--)
            {
                if (StateUnits[i].Info.isStartExec)
                {
                    if (StateUnits[i].Info.isAdditive)
                    {
                        for (int j = 0; j < StateUnits[i].Duration; j++)
                        {
                            StateSystem.Execution(StateUnits[i].Info.buffCMD, transform.gameObject);   
                        }
                    }
                    else
                    {
                        StateSystem.Execution(StateUnits[i].Info.buffCMD, transform.gameObject);
                    }

                    if (StateUnits[i].Decrement())
                    {
                        RemoveState(StateUnits[i]);
                    }
                }
            }
        }

        public void StatesEnd()
        {
            for (int i = StateUnits.Count-1; i > 0; i--)
            {
                if (!StateUnits[i].Info.isStartExec)
                {
                    if (StateUnits[i].Info.isAdditive)
                    {
                        for (int j = 0; j < StateUnits[i].Duration; j++)
                        {
                            StateSystem.Execution(StateUnits[i].Info.buffCMD, transform.gameObject);   
                        }
                    }
                    else
                    {
                        StateSystem.Execution(StateUnits[i].Info.buffCMD, transform.gameObject);
                    }
                }
                if (StateUnits[i].Decrement())
                {
                    RemoveState(StateUnits[i]);
                }
            }
        }

        public StateUnit GetUnitFromID(string id)
        {
            foreach (var unit in StateUnits)
            {
                if (unit.Info.id == id)
                {
                    return unit;
                }
            }
            return null;
        }
    }

}