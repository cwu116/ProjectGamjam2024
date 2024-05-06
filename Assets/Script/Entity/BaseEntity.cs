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
    private AttackUnit_Data _model; //ս����λ�����ڳ�ʼ��

    private EntityType _myType; //ʵ������
    private AttackType _attackType; //��������
    private MaterialType _dropMaterial; //�������
    protected HexCell _curHexCell; //��ǰ���ڵ�ͼ����
    private Vector2 _spawnPoint;  // ������
    private Vector3 _direction; //����
    
    protected BuffComponent buff; //����buff

    private bool bIsObstacle; // ���ϰ���
    private bool bWillDoubleSkill;  //�Ƿ���ⴥ��
    [SerializeField] private bool bisPlayer;  //�Ƿ������

    private int currentHeightIndex;
    private int currentWidthIndex;

    private string spawningPath; //���ɵؿ�����
    public bool isDead;//�Ƿ�����

    protected void InitEntity()
    {
        _myType = _model.entityType;
        _attackType = _model.attackType;
        _dropMaterial = _model.dropMaterial;
        
        buff = GetComponent<BuffComponent>();
        
        buff.RegisterFunc(ActionKey.Die, Die);
        buff.RegisterFunc(TActionKey.Away, Away);
        buff.RegisterFunc(TActionKey.SpawnPath, SpawnPath);
        buff.RegisterFunc(TActionKey.Sleep, Sleep);
        
        buff.RegisterParam(ValueKey.Hp, new ValueInt(_model.hp));
        buff.RegisterParam(ValueKey.MaxHp, new ValueInt(_model.hp));
        buff.RegisterParam(ValueKey.Attack, new ValueInt(_model.Attack));
        buff.RegisterParam(ValueKey.Defence, new ValueInt(0));
        buff.RegisterParam(ValueKey.bInvisible, new ValueInt(0));
        buff.RegisterParam(ValueKey.bMislead, new ValueInt(0));
        buff.RegisterParam(ValueKey.bFlamePure, new ValueInt(0));
        buff.RegisterParam(ValueKey.bIsSilent, new ValueInt(0));
        buff.RegisterParam(ValueKey.HateValue, new ValueInt(0));
        buff.RegisterParam(ValueKey.MoveTimes, new ValueInt(_model.moveTimes));
        buff.RegisterParam(ValueKey.SkillRange, new ValueInt(_model.RangeRight));
        buff.RegisterParam(ValueKey.StepLenghth, new ValueInt(_model.stepLength));
        buff.RegisterParam(ValueKey.WatchRange, new ValueInt(_model.watchRange));
        buff.RegisterParam(ValueKey.MaxMoveTimes, new ValueInt(_model.moveTimes));
        buff.RegisterParam(ValueKey.MinSkillRange, new ValueInt(_model.RangeLeft));

        // Debug.Log("���Գ�ʼ��");
    }

    protected virtual void Start()
    {

    }

    // ���-ֻ��
    public BuffComponent BuffComp
    {
        get { return buff; }
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

    public HexCell LastHexCell
    {
        get;
        set;
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

    public bool IsObstacle
    {
        get => bIsObstacle;
        set => bIsObstacle = value;
    }

    public bool WillDoubleSkill
    {
        get => bWillDoubleSkill;
        set => bWillDoubleSkill = value;
    }

    public bool IsPlayer
    {
        get => bisPlayer;
        set => bisPlayer = value;
    }

    public string SpawningPath
    {
        get => spawningPath;
        set => spawningPath = value;
    }

    public Vector2 SpawnPoint
    {
        get => _spawnPoint;
        set
        {
            _spawnPoint = value;
        }
    }

}