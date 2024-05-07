using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.System;
using Game;

public class BackpackModel : BaseModel
{
    public override void InitModel()
    {
        Register();
       for(int i=0;i<3;i++)
        {
            int index = Random.Range(0, GameBody.GetModel<CompoundModel>().Item_Data.Count);
            AddPotion(GameBody.GetModel<CompoundModel>().Item_Data[index]);
        }
    }

    void Register()
    {
        EventSystem.Register<CraftAddMaterialEvent>(v => { RemoveMatrial(v.item); });
        EventSystem.Register<CraftRemoveMaterialEvent>(v => { AddMaterial(v.item); });
        EventSystem.Register<CraftResultEvent>(v => { AddPotion(v.result); });
        EventSystem.Register<UsePotionEvent>(v => RemovePotion(v.potion));
        EventSystem.Register<AddItemEvent>(v => AddMaterial(v.item, v.count));
        EventSystem.Register<UnlockRecipe>(v => UnlockRecipe(v.potion));
    }

    public List<Item_s> specialMaterials = new List<Item_s>();
    public List<Item_s> normalMaterials = new List<Item_s>();
    public List<Item_Data> potions = new List<Item_Data>();

    public List<Item_Data> unlockRecipe = new List<Item_Data>();

    public void UnlockRecipe(Item_Data potion)
    {
        if (!unlockRecipe.Contains(potion))
            unlockRecipe.Add(potion);
    }

    public void AddMaterial(Item_s item,int count=1)
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
                target.quantity += count;
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
                target.quantity -=1;

            if (target.quantity <= 0)
                specialMaterials.Remove(target);
        }
        else
        {
            Item_s target = normalMaterials.Find(v => v.Id == item.Id);
            if (target == null)
                return;
            else
                target.quantity -=1;

            if (target.quantity <= 0)
                normalMaterials.Remove(target);
        }
    }

    public void AddPotion(Item_Data potion)
    {
        if (potion == null)
            return;
        if (potions.Count >= 5)
            return;
        potions.Add(potion);
    }

    public void RemovePotion(Item_Data potion)
    {
        potions.Remove(potion);
        EventSystem.Send<RefreshBackpackUIEvent>(new RefreshBackpackUIEvent { potions = potions });
    }
}
