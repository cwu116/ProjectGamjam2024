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
    private Vector3 _direction; //方向
    
    protected BuffComponent buff; //自身buff

    private int currentHeightIndex;
    private int currentWidthIndex;

    private void Awake()
    {
        _myType = _model.entityType;
        _attackType = _model.attackType;
        _dropMaterial = _model.dropMaterial;
        
        buff = GetComponent<BuffComponent>();
        
        buff.RegisterFunc(ActionKey.Die, Die);
        buff.RegisterFunc(TActionKey.Away, Away);
        buff.RegisterFunc(TActionKey.SpawnPath, SpawnPath);
        buff.RegisterFunc(TActionKey.Sleep, Sleep);
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

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public HexCell GetCurrentHexCell()
    {
        return this.CurHexCell;
    }

}