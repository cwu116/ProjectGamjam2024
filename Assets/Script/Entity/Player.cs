using System;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Config;
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
        SetModel("Player");
        buff = GetComponent<BuffComponent>();
        
        buff.RegisterFunc(ActionKey.Die, Die);
        buff.RegisterFunc(TActionKey.Away, Away);
        buff.RegisterFunc(TActionKey.SpawnPath, SpawnPath);
        buff.RegisterFunc(TActionKey.Sleep, Sleep);
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
