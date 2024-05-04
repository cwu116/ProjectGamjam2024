using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.System;

public class BackpackModel : BaseModel
{
    public override void InitModel()
    {
        Register();
    }

    void Register()
    {
        EventSystem.Register<CraftAddMaterialEvent>(v => { RemoveMatrial(v.item); });
        EventSystem.Register<CraftRemoveMaterialEvent>(v => { AddMatrial(v.item); });
        EventSystem.Register<CraftResultEvent>(v => { AddPotion(v.result); });
        EventSystem.Register<UsePotionEvent>(v => RemovePotion(v.potion));
    }

    public List<Item_s> specialMaterials = new List<Item_s>();
    public List<Item_s> normalMaterials = new List<Item_s>();
    public List<Item_Data> potions = new List<Item_Data>();

    public void AddMatrial(Item_s item,int count=1)
    {
        if (item.Id.Equals("A") || item.Id.Equals("B") || item.Id.Equals("C") || item.Id.Equals("D"))
        {
           Item_s target=  specialMaterials.Find(v => v.Id == item.Id);
            if (target == null)
            {
                Item_s newItem = new Item_s(item.Id, item.Name, item.Description, count);
                specialMaterials.Add(newItem);
            }
            else
                target.quantity += count;
        }
        else
        {
            Item_s target = normalMaterials.Find(v => v.Id == item.Id);
            if (target == null)
            {
                Item_s newItem = new Item_s(item.Id, item.Name, item.Description, count);
                normalMaterials.Add(newItem);
            }
            else
                target.quantity += 1;
        }
    }

    public void RemoveMatrial(Item_s item)
    {
        if (item.Id.Equals("A") || item.Id.Equals("B") || item.Id.Equals("C") || item.Id.Equals("D"))
        {
            Item_s target = specialMaterials.Find(v => v.Id == item.Id);
            if (target == null)
                return;
            else
                target.quantity = -1;

            if (target.quantity <= 0)
                specialMaterials.Remove(target);
        }
        else
        {
            Item_s target = normalMaterials.Find(v => v.Id == item.Id);
            if (target == null)
                return;
            else
                target.quantity = -1;

            if (target.quantity <= 0)
                normalMaterials.Remove(target);
        }
    }

    public void AddPotion(Item_Data potion)
    {
        potions.Add(potion);
    }

    public void RemovePotion(Item_Data potion)
    {
        potions.Remove(potion);
    }
}
