using Buff;
using Buff.Tool;
using Game;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff.Config;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    private AttackUnit_Data _model; //战斗单位表，用于初始化
    [SerializeField] private int _curHp; //当前生命值

    private ValueInt _maxHp; 
    [SerializeField] private int _restMoveTimes; //剩余行动点
    private ValueInt _defence; //防御力
    private EntityType _myType; //实体类型
    private AttackType _attackType; //攻击类型
    private MaterialType _dropMaterial; //掉落材料
    private HexCell _curHexCell; //当前所在地图格子
    private Vector3 _direction; //方向
    protected BuffComponent buff; //自身buff

    private int currentHeightIndex;
    private int currentWidthIndex;

    public BaseEntity()
    {
        _curHp = _model.hp;
        _maxHp = new ValueInt(_model.hp);
        _restMoveTimes = _model.moveTimes;
        _myType = _model.entityType;
        _attackType = _model.attackType;
        _dropMaterial = _model.dropMaterial;
    }

    private void Awake()
    {
        buff.RegisterFunc(ActionKey.Die, Die);
    }

    // 组件-只读
    public BuffComponent BuffComp
    {
        get { return buff; }
    }

    public int CurHP
    {
        get { return _curHp; }
        set { _curHp = value; }
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

    public int RestMoveTimes
    {
        get { return _restMoveTimes; }
        set { _restMoveTimes = value; }
    }

    public int CurrentHeightIndex
    {
        get { return currentHeightIndex; }
        set { currentHeightIndex = value; }
    }

    public int CurrentWidthIndex
    {
        get { return currentWidthIndex; }
        set { currentWidthIndex = value; }
    }


    public EntityType MyType
    {
        get { return _myType; }
        set { }
    }

    public AttackType MyAttackType
    {
        get { return _attackType; }
        set { }
    }


    public MaterialType DropMaterial
    {
        get { return _dropMaterial; }
        set { _dropMaterial = value; }
    }

    public HexCell CurHexCell
    {
        get { return _curHexCell; }
        set { _curHexCell = value; }
    }

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public HexCell GetCurrentHexCell()
    {
        return this.CurHexCell;
    }

    public bool SetModel(string name)
    {
        AttackUnit_Data ad = GameBody.GetModel<AttackUnitModel>().GetDataByName(name);
        if (ad != null)
        {
            this._model = ad;
            return true;
        }

        return false;
    }

    public bool CanMove()
    {
        if (this.RestMoveTimes <= 0)
            return false;

        return true;
    }

    public virtual void UseSkill(BaseEntity target)
    {
        this.RestMoveTimes--;
    }

    public void GetHurt(int damage)
    {
        int realDamage = damage - this.Defence;
        if (realDamage > 0)
        {
            this.CurHP -= realDamage;
            if (this.CurHP <= 0)
            {
                Die();
            }

            return;
        }

        Debug.Log("没有造成伤害");
    }

    public virtual void Die()
    {
    }
}