using Game.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEntity
{

    public Enemy(string name)
    {
        this.SetModel(name);
    }

    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);

    }
}
