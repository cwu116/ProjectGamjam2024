using System;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff;
using UnityEngine;

public class Player : BaseEntity
{

    private void Awake()
    {
        SetModel("Player");
    }

    private void Start()
    {
        buff = GetComponent<BuffComponent>();
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
