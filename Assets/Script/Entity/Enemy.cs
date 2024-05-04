using System;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using UnityEngine;

public class Enemy : BaseEntity
{

    [SerializeField] private new string name;
    private void Awake()
    {
        buff = GetComponent<BuffComponent>();
        IsPlayer = false;
    }

    private void Start()
    {
        SetModel(name);
        buff.RegisterFunc(ActionKey.Hatred, Hatred);
        
        buff.RegisterFunc(ActionKey.Die, Die);
        buff.RegisterFunc(TActionKey.Away, Away);
        buff.RegisterFunc(TActionKey.SpawnPath, SpawnPath);
        buff.RegisterFunc(TActionKey.Sleep, Sleep);
        
    }
    
    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);
    }

    public override void Hatred()
    {
        base.Hatred();
        Harted = true;
    }
}
