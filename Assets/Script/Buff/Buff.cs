using System;
using System.Collections;
using System.Collections.Generic;
using Game.System;
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
        public List<string>     death;            // 状态亡语

        public bool IsVaild()
        {
            return string.IsNullOrEmpty(id);
        }
    }

    public class StateUnit
    {
        private State info;
        public int Duration;
        public GameObject Target;

        public StateUnit(State theInfo, int theDuration, GameObject target)
        {
            info = theInfo;
            Duration = theDuration;
            Target = target;
        }

        ~StateUnit()
        {
            StateSystem.Execution(info.death, Target);
        }

        public State Info
        {
            get => info;
        }

        /// <summary>
        /// 持续回合递减
        /// </summary>
        /// <returns>bool 是否已小于零</returns>
        public bool Decrement()
        {
            Duration--;
            return Duration < 0;
        } 
    }

}

