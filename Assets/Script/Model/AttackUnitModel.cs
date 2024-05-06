using Assets.Script.Config;
using Game.Util;
using MainLogic.Manager;
using System.Collections;
using System.Collections.Generic;
using Game.Model;
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
        None,
    }
}

public class AttackUnit_Data
{
    public string name; //敌人名称
    public EntityType entityType; //单位类型
    public int hp; //血量
    public int Attack; //攻击力
    public int moveTimes; //行动点
    public int stepLength; //移动力
    public int watchRange; //警觉范围
    public AttackType attackType; //攻击类型
    public int RangeLeft; //范围开始
    public int RangeRight; //范围结束
    public string skillDescription; //技能描述
    public int useSkill; //消耗行动力
    public MaterialType dropMaterial; //材料掉落
}

/// <summary>
/// 敌人表（包括玩家）
/// </summary>
public class AttackUnitModel : BaseModel
{
    public List<AttackUnit_Data> attackUnits = new List<AttackUnit_Data>();

    public override void InitModel()
    {
        attackUnits = JsonUtil.ToObject<List<AttackUnit_Data>>(
            ResourcesManager.LoadText(JsonPath.ItemPath, JsonFileName.AttackUnit));
    }

    public AttackUnit_Data GetDataByName(string name)
    {
        foreach (var n in attackUnits)
        {
            if (n.name == name)
            {
                return n;
            }
        }

        return null;
    }
}
