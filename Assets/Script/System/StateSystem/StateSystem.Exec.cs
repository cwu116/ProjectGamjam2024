using System;
using System.Collections.Generic;
using System.Linq;
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
            Debug.Log(string.Join(' ',CMDlist) + "::" + target.name);
            if (CMDlist.Count == 0)
            {
                return;
            }
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
                    BuffComponent temp = target.GetComponent<BuffComponent>();
                    temp.ValueUnits[Enum.Parse<ValueKey>(args[0].Replace("$",""))].AddValue(ParseParam(args[1], temp), true);
                    target.GetComponent<BaseEntity>().RefreshHpInUI();
                }
                else if (_raw.Contains("+"))
                {
                    List<string> args = new List<string>(_raw.Split(new char[] {'+'}));
                    BuffComponent temp = target.GetComponent<BuffComponent>();
                    temp.ValueUnits[Enum.Parse<ValueKey>(args[0].Replace("$",""))].AddValue(ParseParam(args[1], temp));
                    target.GetComponent<BaseEntity>().RefreshHpInUI();
                }
                else
                {
                    // 解析参数与命令名
                    List<string> args = new List<string>(_raw.Split(new char[] {':'}));
                    // args[0]    :     命令名
                    // args[1]    :     参数数组
                    List<string> Params = new List<string>(args[1].Split(new char[] {','}));
                    BuffComponent temp = null;
                    if (target.TryGetComponent(out BaseEntity entity))
                    {
                        entity = target.GetComponent<BaseEntity>();
                        temp = entity.BuffComp;
                    }
                    switch (Enum.Parse<BuffType>(args[0]))
                    {
                        case BuffType.ChangeValue:
                            if (Params[0] == "Damage")
                            {
                                entity.GetHurt(int.Parse(Params[1]));
                                target.GetComponent<BaseEntity>().RefreshHpInUI();
                            }
                            else
                            {
                                if (Params[1] == "-")
                                {
                                    // - 代表清零
                                    temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])].AddValue(temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])] * -1);
                                    target.GetComponent<BaseEntity>().RefreshHpInUI();
                                }
                                else
                                {
                                    if (bool.Parse(Params[2]))
                                    {
                                        temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])].AddValue(int.Parse(Params[1]));
                                        target.GetComponent<BaseEntity>().RefreshHpInUI();
                                    }
                                    else
                                    {
                                        temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])]
                                            .AddValue(int.Parse(Params[1]), true);
                                        target.GetComponent<BaseEntity>().RefreshHpInUI();
                                    }
                                    Debug.Log(entity.name + ": " + Params[0] + ": " + temp.ValueUnits[Enum.Parse<ValueKey>(Params[0])]);

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
                                Debug.Log("添加状态");
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
                                Path = "Prefabs/MapHexCell/";
                            }
                            else
                            {
                                Path = "Prefabs/Enemy/";
                            }
                            if (Params[2].Contains("["))
                            {
                                List<string> xy_str =
                                    new List<string>(Params[2].Replace("[", "").Replace("]", "").Split(new[] {','}));
                                Vector2 xy = new Vector2(int.Parse(xy_str[0]), int.Parse(xy_str[1]));
                                if (bool.Parse(Params[0]))
                                {
                                    GridManager.Instance.ChangeHexCell(GridManager.Instance.hexCells[(int) xy.x, (int) xy.y], Enum.Parse<HexType>(Params[1]));
                                }
                                else
                                {
                                    BaseEntity.SpawnEntity(ResourcesManager.LoadPrefab(Path, Params[1]), GridManager.Instance.hexCells[(int) xy.x, (int) xy.y]);
                                }
                            }
                            else if (Params[2].Contains("this"))
                            {
                                int[] pos = new int[]{};
                                if (target.TryGetComponent(out HexCell cell))
                                {
                                    pos = new[] {(int)cell.Pos.x, (int)cell.Pos.y };
                                }
                                else
                                {
                                    pos = new[] {(int)target.GetComponent<BaseEntity>().CurHexCell.Pos.x, (int)target.GetComponent<BaseEntity>().CurHexCell.Pos.y };
                                }
                                
                                if (bool.Parse(Params[0]))
                                {
                                    GridManager.Instance.ChangeHexCell(GridManager.Instance.hexCells[pos[0], pos[1]], Enum.Parse<HexType>(Params[1]));
                                }
                                else
                                {
                                    BaseEntity.SpawnEntity(ResourcesManager.LoadPrefab(Path, Params[1]), GridManager.Instance.hexCells[pos[0],pos[1]]);
                                }
                            }
                            else
                            {
                                HexCell targetCell = target.GetComponent<BaseEntity>().CurHexCell;
                                List<HexCell> cellList = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                                    .GetRoundHexCell(targetCell.Pos, int.Parse(Params[2])));
                                foreach (var cell in cellList)
                                {
                                    if (bool.Parse(Params[0]))
                                    {
                                        GridManager.Instance.ChangeHexCell(cell, Enum.Parse<HexType>(Params[1]));
                                    }
                                    else
                                    {
                                        BaseEntity.SpawnEntity(ResourcesManager.LoadPrefab(Path, Params[1]), cell);
                                    }
                                }
                            }

                            break;
                        case BuffType.Action:
                            if (Enum.TryParse(Params[0], out ActionKey outer))
                            {
                                temp.FuncUnits[outer].Invoke();
                            }
                            else if (Enum.TryParse(Params[0], out TActionKey touter))
                            {
                                List<string> innerParams = new List<string>();
                                // params[] = "ResumeHp","3","true","[int=1","string="a"]"
                                for (int i = Params.Count; i > 0; i--)
                                {
                                    innerParams.Add(Params[i-1].Replace("[", "").Replace("]", "").Replace("this",target.name));
                                    if (Params[i-1].Contains("["))
                                    {
                                        break;
                                    }
                                }
                                innerParams.Reverse();
                                Debug.Log(string.Join(' ', innerParams));
                                ParamList list = new ParamList(innerParams);
                                temp.TFuncUnits[touter].Invoke(list);
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
                            Params.RemoveAt(Params.Count-1);

                            string final = string.Join(',', Params);

                            final = final.Replace("[","").Replace("]","");
                            List<string> NewCMDList = new List<string>(final.Split(new char[] {'*'}));
                            // 传入回合延时函数
                            GameBody.GetSystem<StateSystem>().delayStuff.Add(new DelayUnit(NewCMDList, durations, target));
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
                return target.ValueUnits[Enum.Parse<ValueKey>(arg.Replace("$",""))];
            }
            else
            {
                return int.Parse(arg);
            }
        }
    }
}