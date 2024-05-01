using System;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using Buff.Tool;
using Game;
using Game.Model;
using MainLogic.Manager;
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
                    List<string> args = new List<string>(_raw.Split(new char[] {'='}));
                    BuffComponent temp = GameObject.Find("Player").GetComponent<BuffComponent>();
                    temp.ValueUnits[Enum.Parse<ValueKey>(args[0].Remove(0))].AddValue(ParseParam(args[1], temp));
                }
                else if (_raw.Contains("+"))
                {
                    List<string> args = new List<string>(_raw.Split(new char[] {'='}));
                    BuffComponent temp = GameObject.Find("Player").GetComponent<BuffComponent>();
                    temp.ValueUnits[Enum.Parse<ValueKey>(args[0].Remove(0))].AddValue(ParseParam(args[1], temp), true);
                }
                else
                {
                    // 解析参数与命令名
                    List<string> args = new List<string>(_raw.Split(new char[] {':'}));
                    // args[0]    :     命令名
                    // args[1]    :     参数数组
                    List<string> Params = new List<string>(args[1].Split(new char[] {','}));
                    BaseEntity entity = GameObject.Find("Player").GetComponent<BaseEntity>();
                    BuffComponent temp = entity.BuffComp;
                    switch (Enum.Parse<BuffType>(args[0]))
                    {
                        case BuffType.ChangeValue:
                            if (Params[0] == "Damage")
                            {
                                entity.GetHurt(int.Parse(Params[1]));
                                Debug.Log(entity.Hp.ToString());
                            }
                            else
                            {
                                if (Params[1].Contains('-'))
                                {
                                    temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])].AddValue(temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])] * -1);
                                }
                                else
                                {
                                    if (bool.Parse(Params[2]))
                                    {
                                        temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])].AddValue(int.Parse(Params[1]));
                                    }
                                    else
                                    {
                                        temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])]
                                            .AddValue(int.Parse(Params[1]), true);
                                    }
                                }
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
                                    int.Parse(Params[1]) == -1 ? 9999 : int.Parse(Params[1]),
                                    target);
                                if (bool.Parse(Params[2]))
                                {
                                    // 当场生效
                                    Execution(GameBody.GetModel<StateModel>().GetStateFromID(Params[0]).buffCMD,target);
                                }
                            }

                            break;
                        case BuffType.Create:
                            string Path = "";
                            if (bool.Parse(Params[0]))
                            {
                                Path = "Assets/Resources/Prefabs/MapHexCell/";
                            }
                            else
                            {
                                Path = "Assets/Resources/Prefabs/Enemy/";
                            }
                            if (Params[2].Contains("["))
                            {
                                List<string> xy_str =
                                    new List<string>(Params[2].Replace("[", "").Replace("]", "").Split(new[] {','}));
                                Vector2 xy = new Vector2(int.Parse(xy_str[0]), int.Parse(xy_str[1]));
                                GameObject.Instantiate(
                                    ResourcesManager.LoadPrefab(Path,
                                        Params[1] + ".prefab"),
                                    GridManager.Instance.hexCells[(int) xy.x, (int) xy.y].transform.position,
                                    Quaternion.identity);
                            }
                            else if (Params[2].Contains("this"))
                            {
                                GameObject.Instantiate(
                                    ResourcesManager.LoadPrefab(Path,
                                        Params[1] + ".prefab"),
                                    target.transform.position,
                                    Quaternion.identity);
                            }
                            else
                            {
                                HexCell targetCell = target.GetComponent<BaseEntity>().CurHexCell;
                                List<HexCell> cellList = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                                    .GetRoundHexCell(targetCell.Pos, int.Parse(Params[2])));
                                foreach (var cell in cellList)
                                {
                                    GameObject.Instantiate(
                                        ResourcesManager.LoadPrefab(Path,
                                            Params[1] + ".prefab"),
                                        cell.transform.position,
                                        Quaternion.identity);
                                }
                            }

                            break;
                        case BuffType.Action:
                            if (temp.FuncUnits.ContainsKey(Enum.Parse<ActionKey>(Params[0])))
                            {
                                temp.FuncUnits[Enum.Parse<ActionKey>(Params[0])].Invoke();
                            }
                            else if (temp.TFuncUnits.ContainsKey(Enum.Parse<TActionKey>(Params[0])))
                            {
                                List<string> innerParams = new List<string>();
                                // params[] = "ResumeHp","3","true","[int=1","string="a"]"
                                for (int i = Params.Count; i > 0; i--)
                                {
                                    innerParams.Add(Params[i].Replace("[", "").Replace("]", ""));
                                    if (Params[i].Contains("["))
                                    {
                                        break;
                                    }
                                }

                                innerParams.RemoveAt(0);
                                ParamList list = new ParamList(innerParams);
                                temp.TFuncUnits[Enum.Parse<TActionKey>(Params[0])].Invoke(list);
                            }
                            else
                            {
                                Debug.LogError("[Action Error] inValid Func");
                            }

                            break;
                        case BuffType.Delay:
                            args.RemoveAt(0);
                            string CMDs = string.Join(':', args);
                            Params = new List<string>(CMDs.Split(new[] {','}));
                            int durations = int.Parse(Params[^1]);
                            Params.RemoveAt(Params.Count);

                            string final = string.Join(',', Params).Remove(0);
                            final = final.Remove(final.Length - 1);

                            List<string> NewCMDList = new List<string>(final.Split(new char[] {'*'}));

                            // 传入回合延时函数
                            // XXX.DelayFlow(string[] cmdList, int count, GameObject target);

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
                return target.ValueUnits[Enum.Parse<ValueKey>(arg.Remove(0))];
            }
            else
            {
                return int.Parse(arg);
            }
        }
    }
}