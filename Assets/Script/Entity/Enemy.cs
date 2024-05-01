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
    }

    private void Start()
    {
        SetModel(name);
        buff.RegisterFunc(ActionKey.AutoMove, AutoMove);
        buff.RegisterFunc(ActionKey.Hatred, Hatred);
    }

    public void AutoMove() { }
    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);
    }

    public override void Hatred()
    {
        base.Hatred();
    }
}
