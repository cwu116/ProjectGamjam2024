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

public class Enemy : BaseEntity,IPointerClickHandler
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
        if (GameBody.GetModel<PlayerActionModel>().currentPotion != null)
        {
            Debug.Log("use");
            GameBody.GetSystem<PotionUseSystem>().Use(GameBody.GetModel<PlayerActionModel>().currentPotion, this.gameObject);
            return;
        }
    }
}
