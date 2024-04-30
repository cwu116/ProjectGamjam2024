using Game.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    private AttackUnitModel model;
    private int _curHp;
    private int _maxHp;
    public int restMoveTimes;
    private int _attck;
    private EntityType _myType;
    private AttackType _attackType;
    private int _stepLenghth;
    private int _rangeLeft;
    private int _rangeRight;
    private MaterialType _dropMaterial;
    private HexCell _curHexCell;
    private Vector3 _direction;

    private int currentHeightIndex;
    private int currentWidthIndex;

    public BaseEntity()
    {
        _curHp = model.hp;
        _maxHp = model.hp;
        _attck = model.Attack;
        _myType = model.entityType;
        _attackType = model.attackType;
        _stepLenghth = model.stepLength;
        _rangeLeft = model.RangeLeft;
        _rangeRight = model.RangeRight;
        _dropMaterial = model.dropMaterial;
    }

    public int CurHP
    {
        get { return _curHp; }
        set { _curHp = value; }
    }

    public int MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }

    public int Attack
    {
        get { return _attck; }

        set { }
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

    public int StepLength
    {
        get { return _stepLenghth; }
        set { _stepLenghth = value; }
    }

    public int RangeLeft
    {
        get { return _rangeLeft; }
        set { _rangeLeft = value; }
    }

    public int RangeRight
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
    public bool SetModel(string name)
    {
        foreach (AttackUnitModel aum in DataManager.Instance.attackUnits)
        {
            if (aum.name == name)
            {
                this.model = aum;
                return true;
            }
        }

        return false;
    }

    public bool CanMove()
    {
        if (this.restMoveTimes <= 0)
            return false;

        return true;
    }


    public virtual void UseSkill(BaseEntity target)
    {
        this.restMoveTimes--;
    }

    public void GetHurt(int damege)
    {
        this.CurHP -= damege;
        if (this.CurHP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {

    }
}