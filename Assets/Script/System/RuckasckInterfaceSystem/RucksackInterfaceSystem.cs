using Game.Model;
using Game.System;
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
        public List<Item_sModel> materials = new List<Item_sModel>(); // 临时存储
        public CompoundModel compoundModel = new CompoundModel();
        // 添加材料
        public void AddMaterial(Item_sModel item_s)
        {
            if (materials.Count < 3)//合成最大容量
            {
                materials.Add(new Item_sModel(item_s.Name, item_s.Id, item_s.Description, item_s.quantity));
                Debug.Log("添加成功");
                return;
            }
            Debug.LogWarning("只能向List中添加3个物品");
        }

        // 移除材料
        public void RemoveMaterial(Item_sModel item_s)
        {
            if (materials != null || materials.Count >= 0)//移除材料
            {
                materials.Remove(item_s);
                Debug.Log("删除成功");
                return;
            }
            Debug.LogWarning("已经没有物品了");
        }

        // 处理合成操作
        public void Craft()
        {
            EventSystem.Send<Item_sEvent>(new Item_sEvent { Item_sModel = Random_Item() });
        }

        // 触发刷新物品的事件
        public Item_sModel Random_Item()
        {
            List<Item_sModel> list = new List<Item_sModel>();
            Item_sModel item_SModel_1 = materials[0];
            Item_sModel item_SModel_2 = materials[1];
            Item_sModel item_SModel_3 = materials[2];
            foreach (var i in compoundModel.Item_Data)//检索合成表
            {
                if ((i.Material_1.Equals(item_SModel_1) && i.Material_2.Equals(item_SModel_2)) || (i.Material_1.Equals(item_SModel_2) && i.Material_2.Equals(item_SModel_1)))
                    list.Add(new Item_sModel(i.id, i.Name, "", 1));//将满足要求的配方存储起来<需要补充>
            }
            if (item_SModel_3 == null && list != null)
            {
                Debug.Log("随机药物");
                return list[Random.Range(0, list.Count)];//没有特殊配方则随机返回一个药水
            }
            foreach (var i in compoundModel.Item_Data)//在拥有特殊材料的情况下将满足条件的直接返回
            {
                if ((i.Material_1.Equals(item_SModel_1) && i.Material_2.Equals(item_SModel_2) && i.MaterialSpecial.Equals(item_SModel_3)) || (i.Material_1.Equals(item_SModel_2) && i.Material_2.Equals(item_SModel_1) && i.MaterialSpecial.Equals(item_SModel_3)))
                {
                    Debug.Log("生成指定药物");
                    return new Item_sModel(i.id, i.Name, "", 1);//<需要补充>
                }

            }
            Debug.Log("无满足条件的药物");
            return null;//以上都不满足则返回null
        }
        public override void InitSystem()
        {
            throw new global::System.NotImplementedException();
        }
    }
    //————————————————————————————
   
}





