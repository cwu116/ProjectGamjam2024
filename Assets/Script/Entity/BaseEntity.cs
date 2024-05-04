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
    private bool bisPlayer;  //�Ƿ������

    private int currentHeightIndex;
    private int currentWidthIndex;

    private string spawningPath; //���ɵؿ�����

    protected virtual void Start()
    {
        _spawnPoint = new Vector2(){};
            
        _myType = _model.entityType;
        _attackType = _model.attackType;
        _dropMaterial = _model.dropMaterial;
        
        buff = GetComponent<BuffComponent>();
        
        buff.RegisterFunc(ActionKey.Die, Die);
        buff.RegisterFunc(TActionKey.Away, Away);
        buff.RegisterFunc(TActionKey.SpawnPath, SpawnPath);
        buff.RegisterFunc(TActionKey.Sleep, Sleep);
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
        set => _spawnPoint = value;
    }

}