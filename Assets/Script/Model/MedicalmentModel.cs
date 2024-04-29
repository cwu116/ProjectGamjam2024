using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    public enum MaterialColor
    {
        Red,
        Yellow,
        Blue
    }

    public enum EffectType
    {
        Damage,
        ChangeValue,
        Buff,
        Skill,
        Action,
        NULL
    }

    public struct SyntheticMedical
    {
        public MaterialColor m1;
        public MaterialColor m2;
        public MaterialType mt;
    }
    /// <summary>
    /// 药剂
    /// </summary>
    public class MedicalmentModel : BaseModel
    {
        public int id;                         //药剂id
        public string name;                    //药剂名称
        public string materialA;     //合成材料A
        public string materialB;     //合成材料B
        public string specialMaterial;   //特殊材料
        public string effectType;          //类型
        public int[] skillValue;                //技能数值
        public string description;             //效果

        public override void InitModel()
        {
            //TODO:初始化
        }
    }
}

