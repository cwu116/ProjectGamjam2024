using Game.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity
{
    public Player(string name = "Player")
    {
        this.SetModel(name);
    }


    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);

    }

    public void ReleaseMedicalment(BaseEntity target)
    {
        this.RestMoveTimes--;
    }
}
