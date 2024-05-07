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
            if (Duration < 0)
            {
                StateSystem.Execution(info.death, Target);
            }
            return Duration < 0;
        } 
    }

    public class DelayUnit
    {
        public int DelayTime;
        public List<string> DelayBuff;
        public GameObject Target;
        

        public DelayUnit(List<string> buffs, int duration, GameObject target)
        {
            DelayBuff = buffs;
            DelayTime = duration;
            Target = target;
        }
        
        /// <summary>
        /// 递减计时器
        /// </summary>
        /// <returns>bool 是否已为零</returns>
        public bool Decrement()
        {
            DelayTime--;
            if (Target != null && DelayTime <= 0)
            {
                StateSystem.Execution(DelayBuff, Target);
            }
            return DelayTime <= 0;
        }

        ~DelayUnit()
        {
            StateSystem.Execution(DelayBuff, Target);
        }
    }

}

