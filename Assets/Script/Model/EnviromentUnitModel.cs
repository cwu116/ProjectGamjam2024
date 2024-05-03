using Assets.Script.Config;
using Game.Util;
using MainLogic.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    public class EnviromentUnit_Data
    {
        public string name;            //��������
        public int health;             //Ѫ��
        public string skillDescription;//��������
    }
    public class EnviromentUnitModel : BaseModel
    {
        public List<EnviromentUnit_Data> enviromentUnits = new List<EnviromentUnit_Data>();

        public override void InitModel()
        {
            enviromentUnits = JsonUtil.ToObject<List<EnviromentUnit_Data>>(
                    ResourcesManager.LoadText(JsonPath.ItemPath, JsonFileName.AttackUnit));
        }

        public EnviromentUnit_Data GetDataByName(string name)
        {
            foreach (var n in enviromentUnits)
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

