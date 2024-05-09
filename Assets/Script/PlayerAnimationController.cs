using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;
using System;
using DG.Tweening;
using Managers;

public class PlayerAnimationController : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        EventSystem.Register<PlayerMoveEvent>(OnMove);
        EventSystem.Register<UsePotionEvent>(OnUsePotion);
        EventSystem.Register<EntityHurtEvent>(Hurt);
    }

    private void OnDestroy()
    {
        EventSystem.UnRegister<PlayerMoveEvent>(OnMove);
        EventSystem.UnRegister<UsePotionEvent>(OnUsePotion);
        EventSystem.UnRegister<EntityHurtEvent>(Hurt);
    }

    private void Hurt(EntityHurtEvent v)
    {
        if (v.enetity is Player)
        {
            anim.SetTrigger("Hit");
            AudioManager.PlaySound(AudioPath.PlayerHurt);
        }
    }

    private void OnUsePotion(UsePotionEvent potion)
    {
        AudioManager.PlaySound(AudioPath.PlayerAttack);
        anim.SetTrigger("Attack");
    }

    private async void OnMove(PlayerMoveEvent v)
    {
        anim.SetTrigger("Move");
        await System.Threading.Tasks.Task.Delay(500);
        AudioManager.PlaySound(AudioPath.PlayerMove);
        transform.DOMove(v.currentCell.transform.position+Vector3.up*3, 0.5f).SetEase(Ease.Linear);
    }
}
