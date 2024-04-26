using System;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using Game;
using Game.Model;
using UnityEngine;

namespace Game.System
{
    public partial class StateSystem : BaseSystem
    {
        public static void Execution(List<string> CMDlist, GameObject target)
        {
            foreach (var cmdLine in CMDlist)
            {
                string _raw = cmdLine;
                _raw = _raw.Replace(" ", "");
                if (string.IsNullOrEmpty(_raw))
                {
                    return;
                }
                if (_raw.Contains("="))
                {
                    // 赋值
                    string[] args = _raw.Split(new char[] {'='});
                    BuffComponent temp = new BuffComponent(null);
                    if (args[1].Contains("$"))
                    {
                        temp.ValueUnits[args[0].Remove(0)].AddValue(temp.ValueUnits[args[1].Remove(0)]);
                    }
                    else
                    {
                        temp.ValueUnits[args[0].Remove(0)].AddValue(int.Parse(args[1]));
                    }
                }
                else
                {
                    // 解析参数与命令名
                    string[] args = _raw.Split(new char[] {':'});
                    // args[0]    :     命令名
                    // args[1]    :     参数数组
                    string[] Params = args[1].Split(new char[] {','});
                    switch (Enum.Parse<BuffType>(args[0]))
                    {
                        case BuffType.ChangeValue:
                            if (Params[0] == "Damage")
                            {
                                Debug.Log("DamageTrigger");
                                // target.HP  -= args[1].Contains("$") target.buffcomp.ValueUnits[args[1].Remove(0)] ? int.Parse(args[1]  
                            }
                            else
                            {
                                BuffComponent temp = new BuffComponent(null);
                                temp.ValueUnits[Params[0]].AddValue(int.Parse(Params[1]));
                            }
                            break;
                        case BuffType.State:
                            BuffComponent temp1 = new BuffComponent(null);
                            if (args.Length == 1)
                            {
                                temp1.ClearState();
                            }
                            else
                            {
                                temp1.AddState(GameBody.GetModel<StateModel>().GetStateFromID(Params[0]), int.Parse(Params[1]));
                            }
                            break;
                        case BuffType.Create:
                            break;
                        case BuffType.Action: 
                            break;
                    }
                }
            }
        }
    }
}