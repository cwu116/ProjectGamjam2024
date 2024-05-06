using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using DG.Tweening;

public class DropItem : MonoBehaviour
{
    public Item_s item;
    public async void Init(string id, string name)
    {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + id);
        item = new Item_s(id, name, "", 1);

        this.transform.DOMove(Player.instance.transform.position, 1.5f).SetEase(Ease.InQuart);
        await System.Threading.Tasks.Task.Delay(1500);
        GameBody.GetModel<BackpackModel>().AddMaterial(item);
        Destroy(this.gameObject);
    }
}
