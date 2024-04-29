using Buff;
using Buff.Tool;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    private AttackUnitModel _model;     //ս����λ�����ڳ�ʼ��
    [SerializeField]
    private int _curHp;                 //��ǰ����ֵ
    private ValueInt _maxHp;            //����ֵ����
    [SerializeField]
    private int _restMoveTimes;         //ʣ���ж���
    private ValueInt _maxMoveTimes;      //�ж�������
    private ValueInt _attck;            //������
    private ValueInt _defence;          //������
    private EntityType _myType;         //ʵ������
    private AttackType _attackType;     //��������
    private ValueInt _stepLenghth;      //�ƶ���
    private ValueInt _rangeLeft;        //��Χ��ʼ
    private ValueInt _rangeRight;       //��Χ����
    private MaterialType _dropMaterial; //�������
    private HexCell _curHexCell;        //��ǰ���ڵ�ͼ����
    private Vector3 _direction;         //����
    public BuffComponent buff;          //����buff

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
    }

    private void Awake()
    {
        buff.RegisterFunc("Die", Die);
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

    public bool SetModel(string name)
    {
        foreach (AttackUnitModel aum in DataManager.Instance.attackUnits)
        {
            if (aum.name == name)
            {
                this._model = aum;
                return true;
            }
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
        this.CurHP -= damage;
        if (this.CurHP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        
    }
}
