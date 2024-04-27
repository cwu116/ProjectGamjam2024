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
        /// <summary>
        /// 全局指令集
        /// 用于对地块、玩家、敌人等实体的任何操作
        /// [格式：关键字:参数]
        /// 具体代码见"游戏指令"表格
        /// </summary>
        /// <param name="CMDlist">代码行数组</param>
        /// <param name="target">作用对象</param>
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
                    temp.ValueUnits[args[0].Remove(0)].AddValue(ParseParam(args[1], temp));
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

        /// <summary>
        /// 由变量名获取变量值
        /// 仅用于获取，不允许更改，若无则返回解析数值(前提必须是数字字符串)
        /// </summary>
        /// <param name="arg">变量名</param>
        /// <param name="target">引用对象</param>
        /// <returns></returns>
        public static int ParseParam(string arg, BuffComponent target)
        {
            if (arg.Contains("$"))
            {
                return target.ValueUnits[arg.Remove(0)];
            }
            else
            {
                return int.Parse(arg);
            }
        }
    }
}