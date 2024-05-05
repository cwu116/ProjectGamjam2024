using System;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using UnityEngine;
using Game;
using Game.System;

public class Enemy : BaseEntity
{

    [SerializeField] private new string name;
    private void Awake()
    {
        buff = GetComponent<BuffComponent>();
        IsPlayer = false;
    }

    private void Start()
    {
       if (SetModel(name))
        {
            InitEntity();
        }
        
        base.Start();
    }

    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);
        switch (MyAttackType)
        {
            case AttackType.Sword:
                target.GetHurt(Attack);
                break;
            case AttackType.Range:
                break;
            case AttackType.NULL:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }   
    }

    private void OnMouseDown()
    {
        if (GameBody.GetModel<PlayerActionModel>().currentPotion != null)
        {
            Debug.Log("use");
            GameBody.GetSystem<PotionUseSystem>().Use(GameBody.GetModel<PlayerActionModel>().currentPotion, this.gameObject);
            return;
        }
    }

}
