using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    public class EnemyModel : BaseModel
    {
        public string name;            //敌人名称
        public int health;             //血量
        public int actionCount;        //行动点
        public int movingRange;        //移动力
        public int vigilanceRange;     //警觉范围
        public string skillDescription;//技能描述
        public int actionCost;         //消耗行动力
        public char dropMaterial;      //材料掉落

        public override void InitModel()
        {
            //TODO:初始化Model
        }
    }
}

