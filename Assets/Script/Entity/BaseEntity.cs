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
    private AttackUnit_Data _model;     //战斗单位表，用于初始化
    [SerializeField]
    private int _curHp;                 //当前生命值
    private ValueInt _maxHp;            //生命值上限
    [SerializeField]
    private int _restMoveTimes;         //剩余行动点
    private ValueInt _maxMoveTimes;      //行动点上限
    private ValueInt _attck;            //攻击力
    private ValueInt _defence;          //防御力
    private EntityType _myType;         //实体类型
    private AttackType _attackType;     //攻击类型
    private ValueInt _stepLenghth;      //移动力
    private ValueInt _rangeLeft;        //范围开始
    private ValueInt _rangeRight;       //范围结束
    private MaterialType _dropMaterial; //掉落材料
    private HexCell _curHexCell;        //当前所在地图格子
    private Vector3 _direction;         //方向
    public  ValueInt bInvisible;        //隐形
    public ValueInt bMislead;          //误导
    public ValueInt bIsSilent;         //沉默
    public BuffComponent buff;          //自身buff

    public BaseEntity()
    {
        _curHp = _model.hp;
        _maxHp = new ValueInt(_model.hp);
        _attck =new ValueInt ( _model.Attack);
        _restMoveTimes = _model.moveTimes;
        _maxMoveTimes = new ValueInt(_model.moveTimes);
        _myType = _model.entityType;
        _attackType = _model.attackType;
        _stepLenghth =new ValueInt ( _model.stepLength);
        _rangeLeft =new ValueInt ( _model.RangeLeft);
        _rangeRight =new ValueInt ( _model.RangeRight);
        _dropMaterial = _model.dropMaterial;
        bInvisible = new ValueInt (0);
        bMislead = new ValueInt (0);
        bIsSilent = new ValueInt (0);
    }

    private void Awake()
    {
        buff.RegisterFunc(ActionKey.Die, Die);
    }

    public int CurHP
    {
        get { return _curHp; }
        set { _curHp = value; }
    }

    public ValueInt MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }

    public int RestMoveTimes
    {
        get { return _restMoveTimes; }
        set { _restMoveTimes = value; }
    }

    public ValueInt MaxMoveTimes
    {
        get { return _maxMoveTimes; }
        set { _maxMoveTimes = value; }
    }

    public int Attack
    {
        get { return _attck; }

        set { }
    }

    public ValueInt Defence
    {
        get { return _defence; }

        set { _defence = value; }
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

    public ValueInt StepLength
    {
        get { return _stepLenghth; }
        set { _stepLenghth = value; }
    }

    public ValueInt RangeLeft
    {
        get { return _rangeLeft; }
        set { _rangeLeft = value; }
    }

    public ValueInt RangeRight
    {
        get { return _rangeRight; }
        set { _rangeRight = value; }
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

       AttackUnit_Data ad= GameBody.GetModel<AttackUnitModel>().GetDataByName(name);
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