using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buff
{
    public enum BuffType
    {
        ValueChange, State, Create
    }

    [Serializable]
    public struct State
    {
        public string buffName;         // buff名
        public string description;      // buff解释
        public Sprite icon;             // buff图标
        public GameObject target;       // 作用对象
        public int duration;            // 持续回合数
        public string buffCMD;          // buff指令
    }

}

