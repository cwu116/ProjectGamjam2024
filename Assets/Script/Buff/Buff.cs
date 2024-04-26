using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Buff
{

    /// <summary>
    /// 状态类
    /// </summary>
    [Serializable]
    public struct State
    {
        public string           buffName;         // buff名
        public string           id;               // buff_id
        public string           description;      // buff解释
        public List<string>     buffCMD;          // buff指令
        public bool             isAdditive;       // 能否叠加
        public bool             isStartExec;      // 回合开始触发
        

        // 以id形式判断
        public static bool operator ==(State left, State right)
        {
            return left.id == right.id;
        }
        public static bool operator !=(State left, State right)
        {
            return !(left == right);
        }
        
        public static bool operator ==(State left, string right)
        {
            return left == right;
        }

        public static bool operator !=(State left, string right)
        {
            return !(left == right);
        }

        public bool IsVaild()
        {
            return string.IsNullOrEmpty(id);
        }
    }

}

