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
    public string name; //��������
    public EntityType entityType; //��λ����
    public int hp; //Ѫ��
    public int Attack; //������
    public int moveTimes; //�ж���
    public int stepLength; //�ƶ���
    public int watchRange; //������Χ
    public AttackType attackType; //��������
    public int RangeLeft; //��Χ��ʼ
    public int RangeRight; //��Χ����
    public string skillDescription; //��������
    public int useSkill; //�����ж���
    public MaterialType dropMaterial; //���ϵ���
}

/// <summary>
/// ���˱�������ң�
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
