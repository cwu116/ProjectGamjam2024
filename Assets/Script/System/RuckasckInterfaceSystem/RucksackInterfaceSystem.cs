using Game.Model;
using Game.System;
using JetBrains.Annotations;
using RedBjorn.Utils;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//记得Gamebody注册
namespace Game.System
{
    //用于提供合成功能的接口
    public class InventorySystem : BaseSystem
    {
        List<Item_s> materials = new List<Item_s>(); // 临时存储
        CompoundModel compoundModel;
        // 添加材料
        public void AddMaterial(Item_s item_s)
        {
            if (materials.Count < 3)//合成最大容量
            {
                
                if (materials.Count > 1 && materials.Count < 3 && (item_s.Id.Equals("A") || item_s.Id.Equals("B") || item_s.Id.Equals("C") || item_s.Id.Equals("D")))
                {
                    materials.Add(new Item_s(item_s.Name, item_s.Id, item_s.Description, item_s.quantity));

                    Debug.Log("[特殊材料] 添加成功");
                    return;
                }
                else if (materials.Count > 1 && materials.Count < 3)
                {
                    Debug.LogWarning("最后材料只能是特殊材料");
                    return;
                }
                
                if (!(item_s.Id.Equals("A") || item_s.Id.Equals("B") || item_s.Id.Equals("C") || item_s.Id.Equals("D")))
                {
                    materials.Add(new Item_s(item_s.Name, item_s.Id, item_s.Description, item_s.quantity));
                    Debug.Log("[普通材料] 添加成功");
                    return;
                }
                else
                {
                    Debug.LogWarning("前两种材料只能是普通材料");
                    return;
                }

            }
            Debug.LogWarning("只能向List中添加3个物品");
        }

        // 移除材料
        public void RemoveMaterial(Item_s item_s)
        {
            if (materials != null || materials.Count >= 0)//移除材料
            {
                materials.Remove(item_s);
                return;
            }
            Debug.LogWarning("已经没有物品了");
        }

        // 处理合成操作
        public void Craft()
        {
            EventSystem.Send<CraftResultEvent>(new CraftResultEvent { result = Random_Item() });
        }

        // 触发刷新物品的事件
        Item_Data Random_Item()
        {
            List<Item_s> list = new List<Item_s>();
            Item_s item_SModel_1 = null;
            Item_s item_SModel_2 = null;
            Item_s item_SModel_3 = null;
            int sum = 0;
            foreach (var i in materials)
            {
                if (sum == 0)
                {
                    item_SModel_1 = i;
                    sum++;
                    continue;
                }
                if (sum == 1)
                {
                    item_SModel_2 = i;
                    sum++;
                    continue;
                }
                if (sum == 2)
                {
                    item_SModel_2 = i;
                    sum++;
                    continue;
                }
            }
            if (item_SModel_1 != null && item_SModel_2 != null)
            {
                foreach (var i in compoundModel.Item_Data)//检索合成表
                {
                    if ((i.Material_1.Equals(item_SModel_1.Id) && i.Material_2.Equals(item_SModel_2.Id)) || (i.Material_1.Equals(item_SModel_2.Id) && i.Material_2.Equals(item_SModel_1.Id)))
                        list.Add(new Item_s(i.id, i.Name, i.Description, 1));//将满足要求的配方存储起来
                }
                if (item_SModel_3 == null && list != null)
                {
                    Debug.Log("随机药物");
                    var result_ = list[Random.Range(0, list.Count)];
                    var result = compoundModel.Item_Data.Find(v => v.id == result_.Id);
                    EventSystem.Send(new CraftResultEvent() { result = result });
                    return result;//没有特殊配方则随机返回一个药水
                }
                foreach (var i in compoundModel.Item_Data)//在拥有特殊材料的情况下将满足条件的直接返回
                {
                    if ((i.Material_1.Equals(item_SModel_1.Id) && i.Material_2.Equals(item_SModel_2.Id) && i.MaterialSpecial.Equals(item_SModel_3.Id)) || (i.Material_1.Equals(item_SModel_2.Id) && i.Material_2.Equals(item_SModel_1.Id) && i.MaterialSpecial.Equals(item_SModel_3.Id)))
                    {
                        Debug.Log("生成指定药物");
                        var result_ = new Item_s(i.id, i.Name, i.Description, 1);
                        var result = compoundModel.Item_Data.Find(v => v.id == result_.Id);
                        EventSystem.Send(new CraftResultEvent() { result = result });
                        return result;
                    }

                }
            }
            Debug.Log("无满足条件的药物");
            EventSystem.Send(new CraftResultEvent());
            return null;//以上都不满足则返回null
        }
        public override void InitSystem()
        {
            RegisterEvents();
            compoundModel = GameBody.GetModel<CompoundModel>();
        }

        void RegisterEvents()
        {
            EventSystem.Register<CraftTriggerEvent>(v => Craft());
            EventSystem.Register<CraftAddMaterialEvent>(v =>  AddMaterial(v.item));
            EventSystem.Register<CraftRemoveMaterialEvent>(v => RemoveMaterial(v.item));
        }
    }

}
