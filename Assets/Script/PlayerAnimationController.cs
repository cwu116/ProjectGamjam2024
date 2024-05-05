using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;
using System;
using DG.Tweening;

public class PlayerAnimationController : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        EventSystem.Register<PlayerMoveEvent>(v => OnMove(v));
        EventSystem.Register<UsePotionEvent>(v => OnUsePotion(v.potion));
        EventSystem.Register<EntityHurtEvent>(v => Hurt(v));
    }

    private void Hurt(EntityHurtEvent v)
    {
        if (v.enetity is Player)
            anim.SetTrigger("Hurt");
    }

    private void OnUsePotion(Item_Data potion)
    {
        anim.SetTrigger("Attack");
    }

    private async void OnMove(PlayerMoveEvent v)
    {
        anim.SetTrigger("Move");
        await System.Threading.Tasks.Task.Delay(500);
        transform.DOMove(v.currentCell.transform.position, 0.5f).SetEase(Ease.Linear);
    }
}
