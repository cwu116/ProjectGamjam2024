using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game.System;
using Managers;

public class Herb : BaseEntity
{
    public string id;
    public string Name;

    protected override void  Start()
    {
        base.Start();
        Shake();
    }
    async void Shake()
    {
        while(this!=null)
        {
            int interval = 5;
            this.transform.DOShakeRotation(6, 20, 3, 50);
            await System.Threading.Tasks.Task.Delay(5000);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEntity entity = collision.transform.GetComponent<BaseEntity>();

        if (entity != null && entity is Player )
        {
            int count = Random.Range(2, 5);
            Item_s item = new Item_s(id, Name, "", 1);
            EventSystem.Send<AddItemEvent>(new AddItemEvent() { item = item, count = count });
            AudioManager.PlaySound(AudioPath.Collect);
            Destroy(this.gameObject);
        }
    }
}
