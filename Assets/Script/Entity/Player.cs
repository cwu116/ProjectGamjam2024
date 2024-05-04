using System;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using Game;
using UnityEngine;
using Game.System;

public class Player : BaseEntity
{
    public static Player instance;

    private void Awake()
    {
        instance = this;
        IsPlayer = true;
    }

    private void Start()
    {
        base.Start();
        
        buff = GetComponent<BuffComponent>();
        if (SetModel("玩家"))
        {
           InitEntity(); 
        }
        // 测试专用
        _curHexCell = GridManager.Instance.hexCells[1, 1];
        Debug.Log(_curHexCell.Pos);
        transform.position = _curHexCell.transform.position;
        GameBody.GetSystem<MoveSystem>().PlayerMoveTo(gameObject, _curHexCell.Pos);
        EventSystem.Send<PlayerTurnBeginTrigger>();
    }


    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);

    }

    public void ReleaseMedicalment(BaseEntity target)
    {
        MoveTimes.AddValue(-1);
    }
}
