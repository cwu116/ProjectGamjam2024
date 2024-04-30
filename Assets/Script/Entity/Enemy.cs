using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff.Config;
using UnityEngine;

public class Enemy : BaseEntity
{

    public Enemy(string name)
    {
        this.SetModel(name);
    }

    private void Awake()
    {
        buff.RegisterFunc(ActionKey.AutoMove, AutoMove);
    }

    public void AutoMove()
    {

    }
    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);

    }
}
