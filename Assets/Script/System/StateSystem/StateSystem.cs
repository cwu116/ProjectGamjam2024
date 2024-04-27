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

        public override void InitSystem()
        {
            Debug.Log("Init StateSystem");
            stateModel = GameBody.GetModel<StateModel>();
        }

        // 回合开始执行
        public void StateWithStart()
        {
            foreach (var entity in GameObject.FindGameObjectsWithTag("Entity"))
            {
                foreach (var unit in entity.GetComponent<BuffComponent>().StateUnits)
                {
                    if (unit.Key.isStartExec)
                    {
                        Execution(unit.Key.buffCMD, entity);
                        entity.GetComponent<BuffComponent>().StateUnits[unit.Key] -= 1;
                    }
                }
            }
        }
        
        // 回合结束执行
        public void StateWithEnd()
        {
            foreach (var entity in GameObject.FindGameObjectsWithTag("Entity"))
            {
                foreach (var unit in entity.GetComponent<BuffComponent>().StateUnits)
                {
                    if (!unit.Key.isStartExec)
                    {
                        Execution(unit.Key.buffCMD, entity);
                        entity.GetComponent<BuffComponent>().StateUnits[unit.Key] -= 1;
                    }
                    // 状态在回合末尾清除
                    if (entity.GetComponent<BuffComponent>().StateUnits[unit.Key] == 0)
                    {
                        if (unit.Key.death.Count != 0)
                        {
                            Execution(unit.Key.death, entity);
                        }
                        entity.GetComponent<BuffComponent>().RemoveState(unit.Key);
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

