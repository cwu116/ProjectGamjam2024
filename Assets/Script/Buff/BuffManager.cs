using System;
using Buff.Config;
using UnityEngine;

namespace Buff.Manager
{
    public static partial class BuffManager
    {
        public static void Execution(string _raw, GameObject target)
        {
            _raw = _raw.Replace(" ", "");
            if (string.IsNullOrEmpty(_raw))
            {
                return;
            }
            // 解析参数与命令名
            string[] args = _raw.Split(new char[] {':'});
            // args[0]    :     命令名
            // args[1]    :     参数数组
            string[] Params = args[1].Split(new char[] {','});
            switch (Enum.Parse<CmdCode>(args[0]))
            {
                case CmdCode.Damage:
                    Debug.LogFormat("{0}伤害{1}点", target.name, Params[0]);
                    break;
                case CmdCode.MoveRangeChange:
                    Debug.LogFormat("{0}下回合移动{1}{2}点", target.name, int.Parse(Params[0]) > 0 ? "增加" : "减少", Params[0]);
                    break;
                case CmdCode.MaxHpChange:
                    Debug.LogFormat("{0}血量上限{1}{2}点", target.name, int.Parse(Params[0]) > 0 ? "增加" : "减少", Params[0]);
                    break;
                case CmdCode.MoveTimeChange: break;
                case CmdCode.SkillRangeChange: break;
                case CmdCode.DefenceChange: break;
                case CmdCode.StatePush:
                    Debug.LogFormat("{0}获得状态{1}持续{2}回合", target.name, Params[0], Params[1]);
                    break;
                case CmdCode.ClearState: break;
                case CmdCode.CreateEntity: break;
                case CmdCode.CreateScene: break;
            }
        }
    }
}