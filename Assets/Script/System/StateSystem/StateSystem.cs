using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Manager;
using Game.Model;
using UnityEngine;

namespace Game.System
{
    public partial class StateSystem : BaseSystem
    {
        
        // 全局检查玩家状态 
        private StateModel stateModel;

        public override void InitSystem()
        {
            stateModel = GameBody.GetModel<StateModel>();
            startExec = new List<string>()
            {
                "ResumeHp"
            };
            endExec = new List<string>()
            {
                "LiveTarget"
            };
        }

        // 回合开始执行
        public void StateWithStart()
        {
            foreach (var line in stateModel.States)
            {
                foreach (var id in startExec)
                {
                    if (line == id)
                    {
                        // 全局执行状态指令
                        // BuffManager.Execution(line.buffCMD, );
                    }
                }
            }
        }
        
        // 回合结束执行
        public void StateWithEnd()
        {
            foreach (var line in stateModel.States)
            {
                foreach (var id in endExec)
                {
                    if (line == id)
                    {
                        // 全局执行状态指令
                        // BuffManager.Execution(line.buffCMD, );
                    }
                }
            }
        }

        // 为UI提供接口
        //TODO::返回值是一个状态的UI对象预制体数组
        public static List<GameObject> GetStateFromPlayer(GameObject target)
        {
            return null;
        }
    }
}

