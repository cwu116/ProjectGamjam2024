using Buff;
using Buff.Tool;
using Game;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff.Config;
using DG.Tweening;
using UnityEngine;

public partial class BaseEntity : MonoBehaviour
{
    public ValueInt Hp // 生命值
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.Hp))
            {
                comp.ValueUnits.Add(ValueKey.Hp, new ValueInt(_model.hp));
            }
            return comp.ValueUnits[ValueKey.Hp];
        }
    }

    public ValueInt MaxHp // 生命值上限
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.MaxHp))
            {
                comp.ValueUnits.Add(ValueKey.MaxHp, new ValueInt(_model.hp));
            }
            return comp.ValueUnits[ValueKey.MaxHp];
        }
    }

    public ValueInt MoveTimes // 行动点
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.MoveTimes))
            {
                comp.ValueUnits.Add(ValueKey.MoveTimes, new ValueInt(_model.moveTimes));
            }
            return comp.ValueUnits[ValueKey.MoveTimes];
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.MoveTimes))
            {
                comp.ValueUnits[ValueKey.MoveTimes] = value;
            }
        }
    }

    public ValueInt MaxMoveTimes // 行动点上限
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            return comp.ValueUnits[ValueKey.MaxMoveTimes];
        }
    }

    public ValueInt Attack // 攻击力
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.Attack))
            {
                comp.ValueUnits.Add(ValueKey.Attack, new ValueInt(_model.Attack));
            }
            return comp.ValueUnits[ValueKey.Attack];
        }
    }

    public ValueInt Defence // 防御力
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.Defence))
            {
                comp.ValueUnits.Add(ValueKey.Defence, new ValueInt(0));
            }
            return comp.ValueUnits[ValueKey.Defence];
        }
    }

    public ValueInt StepLength // 移动力
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.StepLenghth))
            {
                comp.ValueUnits.Add(ValueKey.StepLenghth, new ValueInt(_model.stepLength));
            }
            return comp.ValueUnits[ValueKey.StepLenghth];
        }

    }

    public ValueInt WatchRange // 警戒圈
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.WatchRange))
            {
                comp.ValueUnits.Add(ValueKey.WatchRange, new ValueInt(_model.watchRange));
            }
            return comp.ValueUnits[ValueKey.WatchRange];
        }
    }
    public ValueInt RangeLeft // 最小技能范围
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.MinSkillRange))
            {
                comp.ValueUnits.Add(ValueKey.MinSkillRange, new ValueInt(_model.RangeLeft));
            }
            return comp.ValueUnits[ValueKey.MinSkillRange];
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.MinSkillRange))
            {
                comp.ValueUnits[ValueKey.MinSkillRange] = value;
            }
        }
    }

    public ValueInt RangeRight // 最大技能范围
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.SkillRange))
            {
                comp.ValueUnits.Add(ValueKey.SkillRange, new ValueInt(_model.RangeRight));
            }
            return comp.ValueUnits[ValueKey.SkillRange];
        }
    }
    public ValueInt HateValue // 仇恨值
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.HateValue))
            {
                comp.ValueUnits.Add(ValueKey.HateValue, new ValueInt(0));
            }
            return comp.ValueUnits[ValueKey.HateValue];
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.HateValue))
            {
                comp.ValueUnits[ValueKey.HateValue] = value;
            }
        }
    }

    public ValueInt bInvisible // bool隐身
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.bInvisible))
            {
                comp.ValueUnits.Add(ValueKey.bInvisible, new ValueInt(0));
            }
            return comp.ValueUnits[ValueKey.bInvisible];
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.bInvisible))
            {
                comp.ValueUnits[ValueKey.bInvisible] = value;
            }
        }
    }

    public ValueInt bMisLead // bool误导
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.bMislead))
            {
                comp.ValueUnits.Add(ValueKey.bMislead, new ValueInt(0));
            }
            return comp.ValueUnits[ValueKey.bMislead];
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.bMislead))
            {
                comp.ValueUnits[ValueKey.bMislead] = value;
            }
        }
    }

    public ValueInt bIsSilent // bool沉默
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.bIsSilent))
            {
                comp.ValueUnits.Add(ValueKey.bIsSilent, new ValueInt(0));
            }
            return comp.ValueUnits[ValueKey.bIsSilent];
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.bIsSilent))
            {
                comp.ValueUnits[ValueKey.bIsSilent] = value;
            }
        }
    }
    
    public ValueInt bFlamePure // bool火焰净化
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (!comp.ValueUnits.ContainsKey(ValueKey.bFlamePure))
            {
                comp.ValueUnits.Add(ValueKey.bFlamePure, new ValueInt(0));
            }
            return comp.ValueUnits[ValueKey.bFlamePure];
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.bFlamePure))
            {
                comp.ValueUnits[ValueKey.bFlamePure] = value;
            }
        }
    }
}