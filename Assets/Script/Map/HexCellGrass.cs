using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexCellGrass : HexCell
{
    public HexCellGrass()
    {
        Type = HexType.Grass;
    }

    BaseEntity inEntity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEntity entity = collision.transform.GetComponent<BaseEntity>();
        if (entity != null && (entity is Player || entity is Enemy))
        {
            inEntity = entity;
            entity.GetComponent<SpriteRenderer>().DOFade(0.3f, 0.3f);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
            collision.GetComponent<SpriteRenderer>().DOFade(1f, 0.2f);
    }
}

