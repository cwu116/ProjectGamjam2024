using System.Collections;
using System.Collections.Generic;
using Assets.Script.Config;
using Buff;
using Game.Util;
using MainLogic.Manager;
using UnityEngine;

namespace Game.Model
{
    public class StateModel : BaseModel
    {
        public List<State> States;

        public override void InitModel()
        {
            string json = ResourcesManager.LoadText(JsonPath.StatePath,JsonFileName.StateName);
            States = JsonUtil.ToObject<List<State>>(json);
        }
    }
}

