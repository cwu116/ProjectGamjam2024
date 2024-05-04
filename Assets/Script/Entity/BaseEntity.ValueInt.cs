using Buff;
using Buff.Tool;
using Game;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff.Config;
using UnityEngine;

public partial class BaseEntity : MonoBehaviour
{
    public ValueInt Hp // 生命值
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.Hp))
            {
                return comp.ValueUnits[ValueKey.Hp];
            }

            return new ValueInt(_model.hp);
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.Hp))
            {
                comp.ValueUnits[ValueKey.Hp] = value;
            }
        }
    }

    public ValueInt MaxHp // 生命值上限
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.MaxHp))
            {
                return comp.ValueUnits[ValueKey.MaxHp];
            }

            return new ValueInt(_model.hp);
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.MaxHp))
            {
                comp.ValueUnits[ValueKey.MaxHp] = value;
            }
        }
    }

    public ValueInt MoveTimes // 行动点
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.MoveTimes))
            {
                return comp.ValueUnits[ValueKey.MoveTimes];
            }

            return new ValueInt(_model.moveTimes);
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
            if (comp.ValueUnits.ContainsKey(ValueKey.MaxMoveTimes))
            {
                return comp.ValueUnits[ValueKey.MaxMoveTimes];
            }

            return new ValueInt(_model.moveTimes);
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.MaxMoveTimes))
            {
                comp.ValueUnits[ValueKey.MaxMoveTimes] = value;
            }
        }
    }

    public ValueInt Attack // 攻击力
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.Attack))
            {
                return comp.ValueUnits[ValueKey.Attack];
            }

            return new ValueInt(_model.Attack);
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.Attack))
            {
                comp.ValueUnits[ValueKey.Attack] = value;
            }
        }
    }

    public ValueInt Defence // 防御力
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.Defence))
            {
                return comp.ValueUnits[ValueKey.Defence];
            }

            return new ValueInt(0);
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.Defence))
            {
                comp.ValueUnits[ValueKey.Defence] = value;
            }
        }
    }

    public ValueInt StepLength // 移动力
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.StepLenghth))
            {
                return comp.ValueUnits[ValueKey.StepLenghth];
            }

            return new ValueInt(_model.stepLength);
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.StepLenghth))
            {
                comp.ValueUnits[ValueKey.StepLenghth] = value;
            }
        }
    }

    public ValueInt WatchRange // 警戒圈
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.WatchRange))
            {
                return comp.ValueUnits[ValueKey.WatchRange];
            }

            return new ValueInt(_model.watchRange);
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.WatchRange))
            {
                comp.ValueUnits[ValueKey.WatchRange] = value;
            }
        }
    }
    public ValueInt RangeLeft // 最小技能范围
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.MinSkillRange))
            {
                return comp.ValueUnits[ValueKey.MinSkillRange];
            }

            return new ValueInt(_model.RangeLeft);
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
            if (comp.ValueUnits.ContainsKey(ValueKey.SkillRange))
            {
                return comp.ValueUnits[ValueKey.SkillRange];
            }

            return new ValueInt(_model.RangeRight);
        }
        set
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.SkillRange))
            {
                comp.ValueUnits[ValueKey.SkillRange] = value;
            }
        }
    }
    public ValueInt HateValue // 仇恨值
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey(ValueKey.HateValue))
            {
                return comp.ValueUnits[ValueKey.HateValue];
            }

            return new ValueInt(_model.RangeRight);
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
            if (comp.ValueUnits.ContainsKey(ValueKey.bInvisible))
            {
                return comp.ValueUnits[ValueKey.bInvisible];
            }

            return new ValueInt(0);
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
            if (comp.ValueUnits.ContainsKey(ValueKey.bMislead))
            {
                return comp.ValueUnits[ValueKey.bMislead];
            }

            return new ValueInt(0);
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
            if (comp.ValueUnits.ContainsKey(ValueKey.bIsSilent))
            {
                return comp.ValueUnits[ValueKey.bIsSilent];
            }

            return new ValueInt(0);
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
            if (comp.ValueUnits.ContainsKey(ValueKey.bFlamePure))
            {
                return comp.ValueUnits[ValueKey.bFlamePure];
            }

            return new ValueInt(0);
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