using System;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using Game;
using UnityEngine;
using Game.System;
using UnityEngine.EventSystems;
using EventSystem = Game.System.EventSystem;
using Managers;

public class Player : BaseEntity,IPointerClickHandler
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
        RefreshHpInUI();
        EventSystem.Send<PlayerTurnBeginTrigger>();
        EventSystem.Register<UsePotionEvent>(v => ReleaseMedicalment());
    }
    

    // 投掷药水
    public void ReleaseMedicalment(/*BaseEntity target*/)
    {
        MoveTimes.AddValue(-1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameBody.GetModel<PlayerActionModel>().CurrentPotion != null && Player.instance.MoveTimes>0)
        {
            Debug.Log("use");
            GameBody.GetSystem<PotionUseSystem>().Use(GameBody.GetModel<PlayerActionModel>().CurrentPotion, this.gameObject);
            AudioManager.PlaySound(AudioPath.Effect);
            return;
        }
    }
}
