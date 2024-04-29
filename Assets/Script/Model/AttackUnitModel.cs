using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    public enum MaterialType
    {
        A,
        B,
        C,
        D,
        NULL
    }
    public enum EntityType
    {
        Enemy,
        Player,
        MapEntity
    }
    public enum AttackType
    {
        Sword,
        Range,
        NULL,
    }
    /// <summary>
    /// 敌人表（包括玩家）
    /// </summary>
    public class AttackUnitModel : BaseModel
    {
        public string name;            //敌人名称
        public EntityType entityType;  //单位类型
        public int hp;                 //血量
        public int Attack;             //攻击力
        public int moveTimes;          //行动点
        public int stepLength;         //移动力
        public int watchRange;         //警觉范围
        public AttackType attackType;  //攻击类型
        public int RangeLeft;          //范围开始
        public int RangeRight;         //范围结束
        public string skillDescription;//技能描述
        public int useSkill;           //消耗行动力
        public MaterialType dropMaterial;      //材料掉落

        public override void InitModel()
        {
            //TODO:初始化
        }
    }
}

