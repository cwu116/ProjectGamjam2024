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
    private AttackUnit_Data _model; //战斗单位表，用于初始化

    private EntityType _myType; //实体类型
    private AttackType _attackType; //攻击类型
    private MaterialType _dropMaterial; //掉落材料
    protected HexCell _curHexCell; //当前所在地图格子
    private Vector2 _spawnPoint;  // 出生点
    private Vector3 _direction; //方向
    
    protected BuffComponent buff; //自身buff

    private bool bIsObstacle; // 是障碍物
    private bool bWillDoubleSkill;  //是否额外触发
    [SerializeField] private bool bisPlayer;  //是否是玩家

    private int currentHeightIndex;
    private int currentWidthIndex;

    private string spawningPath; //生成地块属性
    public bool isDead;//是否死亡

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

        // Debug.Log("属性初始化");
    }

    protected virtual void Start()
    {

    }

    // 组件-只读
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