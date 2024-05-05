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
        EventSystem.Send<PlayerTurnBeginTrigger>();
    }
    

    // 投掷药水
    public void ReleaseMedicalment(BaseEntity target)
    {
        MoveTimes.AddValue(-1);
    }
    
}
