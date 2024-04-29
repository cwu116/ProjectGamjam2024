using System;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using Buff.Tool;
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
                    List<string> args = new List<string>(_raw.Split(new char[] {'='}));
                    BuffComponent temp = GameObject.Find("Player").GetComponent<BuffComponent>();
                    temp.ValueUnits[args[0].Remove(0)].AddValue(ParseParam(args[1], temp));
                }
                else
                {
                    // 解析参数与命令名
                    List<string> args = new List<string>(_raw.Split(new char[] {':'}));
                    // args[0]    :     命令名
                    // args[1]    :     参数数组
                    List<string> Params = new List<string>(args[1].Split(new char[] {','}));
                    BuffComponent temp = GameObject.Find("Player").GetComponent<BuffComponent>();
                    switch (Enum.Parse<BuffType>(args[0]))
                    {
                        case BuffType.ChangeValue:
                            if (Params[0] == "Damage")
                            {
                                Debug.Log("DamageTrigger");
                                temp.ValueUnits["HP"] = new ValueInt(temp.Get("HP") - int.Parse(Params[1]));
                                Debug.Log(temp.ValueUnits["HP"].ToString());
                            }
                            else
                            {
                                temp.ValueUnits[Params[0]].AddValue(int.Parse(Params[1]));
                            }

                            break;
                        case BuffType.State:
                            if (args.Count == 1)
                            {
                                temp.ClearState();
                            }
                            else
                            {
                                temp.AddState(GameBody.GetModel<StateModel>().GetStateFromID(Params[0]),
                                    int.Parse(Params[1]) == -1 ? 9999 : int.Parse(Params[1]));
                            }

                            break;
                        case BuffType.Create:
                            // 对接：获取地块坐标 + 生成实体
                            break;
                        case BuffType.Action:
                            if (temp.FuncUnits.ContainsKey(Params[0]))
                            {
                                temp.FuncUnits[Params[0]].Invoke();
                            }
                            else if (temp.TFuncUnits.ContainsKey(Params[0]))
                            {
                                List<string> InnerParams = new List<string>();
                                // params[] = "ResumeHp","3","true","[int=1","string="a"]"
                                for (int i = Params.Count; i > 0; i--)
                                {
                                    InnerParams.Add(Params[i].Replace("[", "").Replace("]", ""));
                                    if (Params[i].Contains("["))
                                    {
                                        break;
                                    }
                                }

                                InnerParams.RemoveAt(0);
                                ParamList list = new ParamList(InnerParams);
                                temp.TFuncUnits[Params[0]].Invoke(list);
                            }
                            else
                            {
                                Debug.LogError("[Action Error] inValid Func");
                            }
                            break;
                        default:
                            if (args[0] == "Delay")
                            {
                                args.RemoveAt(0);
                                string CMDs = string.Join(':', args);
                                Params = new List<string>(CMDs.Split(new[] {','}));
                                int durations = int.Parse(Params[^1]);
                                Params.RemoveAt(Params.Count);

                                List<string> NewCMDList = new List<string>(
                                    string.Join(',', Params)
                                    .Replace("[", "")
                                    .Replace("]", "")
                                    .Split(new char[] {','}));
                                
                                // 传入回合延时函数
                                // XXX.DelayFlow(string[] cmdList, int count, GameObject target);
                            }
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