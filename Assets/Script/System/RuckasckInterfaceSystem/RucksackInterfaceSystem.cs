using Game.Model;
using Game.System;
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
        public List<Item_s> materials = new List<Item_s>(); // ��ʱ�洢
        public CompoundModel compoundModel = new CompoundModel();
        // ��Ӳ���
        public void AddMaterial(Item_s item_s)
        {
            if (materials.Count < 3)//�ϳ��������
            {
                materials.Add(new Item_s(item_s.Name, item_s.Id, item_s.Description, item_s.quantity));
                Debug.Log("��ӳɹ�");
                return;
            }
            Debug.LogWarning("ֻ����List�����3����Ʒ");
        }

        // �Ƴ�����
        public void RemoveMaterial(Item_s item_s)
        {
            if (materials != null || materials.Count >= 0)//�Ƴ�����
            {
                materials.Remove(item_s);
                Debug.Log("ɾ���ɹ�");
                return;
            }
            Debug.LogWarning("�Ѿ�û����Ʒ��");
        }

        // ����ϳɲ���
        public void Craft()
        {
            EventSystem.Send<Item_sEvent>(new Item_sEvent { Item_sModel = Random_Item() });
        }

        // ����ˢ����Ʒ���¼�
        public Item_s Random_Item()
        {
            List<Item_s> list = new List<Item_s>();
            Item_s item_SModel_1 = materials[0];
            Item_s item_SModel_2 = materials[1];
            Item_s item_SModel_3 = materials[2];
            foreach (var i in compoundModel.Item_Data)//�����ϳɱ�
            {
                if ((i.Material_1.Equals(item_SModel_1.Id) && i.Material_2.Equals(item_SModel_2.Id)) || (i.Material_1.Equals(item_SModel_2.Id) && i.Material_2.Equals(item_SModel_1.Id)))
                    list.Add(new Item_s(i.id, i.Name, i.Description, 1));//������Ҫ����䷽�洢����
            }
            if (item_SModel_3 == null && list != null)
            {
                Debug.Log("���ҩ��");
                return list[Random.Range(0, list.Count)];//û�������䷽���������һ��ҩˮ
            }
            foreach (var i in compoundModel.Item_Data)//��ӵ��������ϵ�����½�����������ֱ�ӷ���
            {
                if ((i.Material_1.Equals(item_SModel_1.Id) && i.Material_2.Equals(item_SModel_2.Id) && i.MaterialSpecial.Equals(item_SModel_3.Id)) || (i.Material_1.Equals(item_SModel_2.Id) && i.Material_2.Equals(item_SModel_1.Id) && i.MaterialSpecial.Equals(item_SModel_3.Id)))
                {
                    Debug.Log("����ָ��ҩ��");
                    return new Item_s(i.id, i.Name, i.Description, 1);
                }

            }
            Debug.Log("������������ҩ��");
            return null;//���϶��������򷵻�null
        }
        public override void InitSystem()
        {

            // throw new global::System.NotImplementedException();
        }
    }

}
