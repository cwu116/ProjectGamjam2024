using Assets.Script.Config;
using Game.Util;
using MainLogic.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    public class EnviromentEffect_Data
    {
        public string name;             //环境效果名
        public string description;      //效果描述
    }
    /// <summary>
    /// 环境效果
    /// </summary>
    public class EnviromentEffctModel : BaseModel
    {
        public List<EnviromentEffect_Data> enviromentEffects = new List<EnviromentEffect_Data>();
        public override void InitModel()
        {
            enviromentEffects = JsonUtil.ToObject<List<EnviromentEffect_Data>>(
                    ResourcesManager.LoadText(JsonPath.ItemPath, JsonFileName.AttackUnit));
        }

        public EnviromentEffect_Data GetDataByName(string name)
        {
            foreach (var n in enviromentEffects)
            {
                if (n.name == name)
                {
                    return n;
                }
            }

            return null;
        }
    }
}

