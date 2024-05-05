using Game.Model;
using Game.System;
using JetBrains.Annotations;
using RedBjorn.Utils;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//�ǵ�Gamebodyע��
namespace Game.System
{
    //�����ṩ�ϳɹ��ܵĽӿ�
    public class InventorySystem : BaseSystem
    {
        List<Item_s> materials = new List<Item_s>(); // ��ʱ�洢
        CompoundModel compoundModel;
        // ��Ӳ���
        public void AddMaterial(Item_s item_s)
        {
            if (materials.Count < 3)//�ϳ��������
            {
                
                if (materials.Count > 1 && materials.Count < 3 && (item_s.Id.Equals("A") || item_s.Id.Equals("B") || item_s.Id.Equals("C") || item_s.Id.Equals("D")))
                {
                    materials.Add(new Item_s(item_s.Name, item_s.Id, item_s.Description, item_s.quantity));

                    Debug.Log("[�������] ��ӳɹ�");
                    return;
                }
                else if (materials.Count > 1 && materials.Count < 3)
                {
                    Debug.LogWarning("������ֻ�����������");
                    return;
                }
                
                if (!(item_s.Id.Equals("A") || item_s.Id.Equals("B") || item_s.Id.Equals("C") || item_s.Id.Equals("D")))
                {
                    materials.Add(new Item_s(item_s.Name, item_s.Id, item_s.Description, item_s.quantity));
                    Debug.Log("[��ͨ����] ��ӳɹ�");
                    return;
                }
                else
                {
                    Debug.LogWarning("ǰ���ֲ���ֻ������ͨ����");
                    return;
                }

            }
            Debug.LogWarning("ֻ����List�����3����Ʒ");
        }

        // �Ƴ�����
        public void RemoveMaterial(Item_s item_s)
        {
            if (materials != null || materials.Count >= 0)//�Ƴ�����
            {
                materials.Remove(item_s);
                return;
            }
            Debug.LogWarning("�Ѿ�û����Ʒ��");
        }

        // ����ϳɲ���
        public void Craft()
        {
            EventSystem.Send<CraftResultEvent>(new CraftResultEvent { result = Random_Item() });
        }

        // ����ˢ����Ʒ���¼�
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
                foreach (var i in compoundModel.Item_Data)//�����ϳɱ�
                {
                    if ((i.Material_1.Equals(item_SModel_1.Id) && i.Material_2.Equals(item_SModel_2.Id)) || (i.Material_1.Equals(item_SModel_2.Id) && i.Material_2.Equals(item_SModel_1.Id)))
                        list.Add(new Item_s(i.id, i.Name, i.Description, 1));//������Ҫ����䷽�洢����
                }
                if (item_SModel_3 == null && list != null)
                {
                    Debug.Log("���ҩ��");
                    var result_ = list[Random.Range(0, list.Count)];
                    var result = compoundModel.Item_Data.Find(v => v.id == result_.Id);
                    EventSystem.Send(new CraftResultEvent() { result = result });
                    return result;//û�������䷽���������һ��ҩˮ
                }
                foreach (var i in compoundModel.Item_Data)//��ӵ��������ϵ�����½�����������ֱ�ӷ���
                {
                    if ((i.Material_1.Equals(item_SModel_1.Id) && i.Material_2.Equals(item_SModel_2.Id) && i.MaterialSpecial.Equals(item_SModel_3.Id)) || (i.Material_1.Equals(item_SModel_2.Id) && i.Material_2.Equals(item_SModel_1.Id) && i.MaterialSpecial.Equals(item_SModel_3.Id)))
                    {
                        Debug.Log("����ָ��ҩ��");
                        var result_ = new Item_s(i.id, i.Name, i.Description, 1);
                        var result = compoundModel.Item_Data.Find(v => v.id == result_.Id);
                        EventSystem.Send(new CraftResultEvent() { result = result });
                        return result;
                    }

                }
            }
            Debug.Log("������������ҩ��");
            EventSystem.Send(new CraftResultEvent());
            return null;//���϶��������򷵻�null
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
