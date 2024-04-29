using System.Collections;
using System.Collections.Generic;
using Assets.Script.Config;
using Buff;
using Game.Util;
using MainLogic.Manager;
using UnityEngine;

namespace Game.Model
{
    /// <summary>
    /// 状�?
    /// </summary>
    public class StateModel : BaseModel
    {
        public List<State> States;

        public override void InitModel()
        {
            Debug.Log("InitModel");
            string json = ResourcesManager.LoadText(JsonPath.StatePath,JsonFileName.StateName);
            States = JsonUtil.ToObject<List<State>>(json);
            Debug.Log(States[0].buffName);
        }
        
        //通过ID获取State数据
        public State GetStateFromID(string id)
        {
            foreach (var state in States)
            {
                if (state == id)
                {
                    return state;
                }
            }   
            return new State();
        }
    }
}

