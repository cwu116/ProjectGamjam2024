using System.Collections;
using System.Collections.Generic;
using Buff;
using Game.Model;
using UnityEngine;

namespace Game.System
{
    public class StateSystem : BaseSystem
    {
        
        // 全局检查玩家状态 

        private StateModel stateModel;

        public override void InitSystem()
        {
            stateModel = GameBody.GetModel<StateModel>();
        }

        // 回合开始执行
        public void StateWithStart()
        {
            // 获取全局对象
            // 遍历对象，获取buff接口
            // 判断是否是回合开始执行，若是，执行
        }
        
        // 回合结束执行
        public void StateWithEnd()
        {
            // 获取全局对象
            // 遍历对象，获取buff接口
            // 判断是否是回合结束执行，若是，执行
        }

        // 为UI提供接口
        //TODO::返回值是一个状态的UI对象预制体数组
        public static List<GameObject> GetStateFromPlayer(GameObject target)
        {
            return null;
        }
    }
}

