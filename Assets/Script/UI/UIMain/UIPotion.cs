using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.System;
using System.Collections.Generic;
using Game.Model;

namespace Game.UI
{
    public class UIPotion : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
    {
        Image icon;
        Item_Data potion;
        UIMain owner;
        private void Awake()
        {
            icon = GetComponent<Image>();
        }

        public void Init(Item_Data potion,UIMain owner)
        {

            Sprite sprite;
            if(potion.Name.Equals("����"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if(potion.Name.Equals("�������"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("����"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("�Ļ�"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("�˷ܶ�ҩ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("��ʮ����"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("��ħ���"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("��ħ����"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("�����"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("�����"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("����"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("���볾��"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("Ӽ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("Ѫ��ף��"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("ȼ��ƿ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("��Ϣ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("�ڰ�֮��"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("����"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("���澻��"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("��������"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("���ۺ�"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("ʵ����1��"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("ʵ����2��"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("ʵ����3��"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else if (potion.Name.Equals("���ҩ��"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }
            else
            {
                sprite= Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
            }

            this.icon.sprite = sprite;
            this.potion = potion;
            this.owner = owner;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameBody.GetModel<PlayerActionModel>().CurrentPotion != this.potion)
            {
                Game.System.EventSystem.Send<OnPotionClick>(new OnPotionClick() { potion = potion });
            }
            if (Player.instance.MoveTimes <= 0)
                return;
            GameBody.GetSystem<MapSystem>().HighLightCells(Player.instance.RangeRight);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            owner.descriptionUI.Show(this.potion,this.icon.sprite);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            owner.descriptionUI.Close();
        }
    }
}
