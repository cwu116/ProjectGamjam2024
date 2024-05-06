using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game.System;

public class Herb : BaseEntity
{
    public string id;
    public string Name;

    void Shake()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEntity entity = collision.transform.GetComponent<BaseEntity>();

        if (entity != null && entity is Player )
        {
            int count = Random.Range(2, 5);
            Item_s item = new Item_s(id, Name, "", 1);
            EventSystem.Send<AddItemEvent>(new AddItemEvent() { item = item, count = count });
            Destroy(this.gameObject);
        }
    }
}
