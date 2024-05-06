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

public class Enemy : BaseEntity,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Animator anim;
    [SerializeField] private new string name;
    private void Awake()
    {
        buff = GetComponent<BuffComponent>();
        anim = GetComponent<Animator>();
        IsPlayer = false;
    }

    private void Start()
    {
        if (SetModel(name))
        {
            InitEntity();
        }
        
        base.Start();
        RefreshHpInUI();
    }

    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);
        switch (MyAttackType)
        {
            case AttackType.Sword:
                target.GetHurt(Attack);
                target.RefreshHpInUI();
                break;
            case AttackType.Range:
                target.GetHurt(Attack);
                break;
            case AttackType.NULL:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        anim.SetTrigger("Attack");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameBody.GetModel<PlayerActionModel>().CurrentPotion != null)
        {
            if (GameBody.GetSystem<MapSystem>().CalculateDistance(Player.instance.CurHexCell.Pos, this.CurHexCell.Pos) > Player.instance.RangeRight)
                return;
            GameBody.GetSystem<PotionUseSystem>().Use(GameBody.GetModel<PlayerActionModel>().CurrentPotion, this.gameObject);
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
