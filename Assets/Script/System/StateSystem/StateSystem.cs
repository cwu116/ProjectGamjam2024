using System;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Game.Model;
using UnityEngine;

namespace Game.System
{
    public partial class StateSystem : BaseSystem, IState
    {
        // 全局检查玩家状态 
        private StateModel stateModel;
        private List<DelayUnit> delayStuff;

        public override void InitSystem()
        {
            stateModel = GameBody.GetModel<StateModel>();
            delayStuff = new List<DelayUnit>();
        }

        // 延时检查装置 ：加在回合初
        public void CheckForDelay()
        {
            foreach (var stuff in delayStuff)
            {
                if (stuff.Decrement())
                {
                    delayStuff.Remove(stuff);
                }
            }
        }

        public void PlayerStatesStart()
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<BuffComponent>().StatesStart();
        }

        public void PlayerStatesEnd()
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<BuffComponent>().StatesEnd();
        }

        public void EnemyStatesStart()
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<BuffComponent>().StatesStart();
            }
        }

        public void EnemyStatesEnd()
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<BuffComponent>().StatesEnd();
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