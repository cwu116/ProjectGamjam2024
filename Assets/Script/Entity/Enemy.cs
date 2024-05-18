using System;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using UnityEngine;
using Game;
using Game.System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

public class Enemy : BaseEntity,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Animator anim;
    [SerializeField] protected string enemyName;

    public string Name
    {
        get => enemyName;
        set => enemyName = value;
    }

    public string dropedItemId;
    public string dropedItemName;

    public bool isBoss;
    private void Awake()
    {
        buff = GetComponent<BuffComponent>();
        anim = GetComponent<Animator>();
        IsPlayer = false;
    }

    private void Start()
    {
        if (SetModel(enemyName))
        {
            InitEntity();
        }
        
        base.Start();
        RefreshHpInUI();

        //HateValue.AddValue(10);
    }

    bool cd=true;

    public async override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);
        switch (MyAttackType)
        {
            case AttackType.Sword:
                anim.SetTrigger("Attack");
                await System.Threading.Tasks.Task.Delay(300);
                if (isBoss)
                    AudioManager.PlaySound(AudioPath.BossAttack);
                else
                    AudioManager.PlaySound(AudioPath.ShortAttack);
                target.GetHurt(Attack);
                target.RefreshHpInUI();
                break;
            case AttackType.Range:
                cd = !cd;
                if(!cd)
                {
                    anim.SetTrigger("Attack");
                    await System.Threading.Tasks.Task.Delay(300);
                    AudioManager.PlaySound(AudioPath.RangeAttack);
                    target.GetHurt(Attack);
                    target.RefreshHpInUI();
                }
                break;
            case AttackType.None:
                break;
        }
    }


    public async void OnPointerClick(PointerEventData eventData)
    {
        if (GameBody.GetModel<PlayerActionModel>().CurrentPotion != null)
        {
            if (GameBody.GetSystem<MapSystem>().CalculateDistance(Player.instance.CurHexCell.Pos, this.CurHexCell.Pos) > Player.instance.RangeRight)
                return;
            if (Player.instance.MoveTimes <= 0)
                return;
            GameBody.GetSystem<PotionUseSystem>().Use(GameBody.GetModel<PlayerActionModel>().CurrentPotion, this.gameObject);
            await System.Threading.Tasks.Task.Delay(1200);
            AudioManager.PlaySound(AudioPath.EnemyEffect);
            return;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            Game.System.EventSystem.Send<ClearAttackBlockEvent>();
            Game.System.EventSystem.Send<ClearWarningBlockEvent>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.isDisturbed)
            Game.System.EventSystem.Send<HighLightAttackBlockEvent>(new HighLightAttackBlockEvent { pos = CurHexCell.Pos, distance = this.RangeRight });
        else
            Game.System.EventSystem.Send<HighLightWarningBlockEvent>(new HighLightWarningBlockEvent { pos = CurHexCell.Pos, distance = this.WatchRange });
    }
}
