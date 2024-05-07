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
            if(potion.Name.Equals("±Þ×Ó"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ºì");
            }
            else if(potion.Name.Equals("ÈâÌå¸ÄÔì"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ºì");
            }
            else if (potion.Name.Equals("Á¦Á¿"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ºì");
            }
            else if (potion.Name.Equals("ÓÄ»ê"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ºì");
            }
            else if (potion.Name.Equals("ÐË·Ü¶¾Ò©"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "·Û");
            }
            else if (potion.Name.Equals("ÎåÊ®²½Éß"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "·Û");
            }
            else if (potion.Name.Equals("¶ñÄ§ë·Áî"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "·Û");
            }
            else if (potion.Name.Equals("¶ñÄ§Á¦Á¿"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "·Û");
            }
            else if (potion.Name.Equals("»î°Ð×Ó"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "³È");
            }
            else if (potion.Name.Equals("»î°Ð×Ó"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "³È");
            }
            else if (potion.Name.Equals("¶¬Ãß"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "³È");
            }
            else if (potion.Name.Equals("ÒþÈë³¾ÑÌ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "³È");
            }
            else if (potion.Name.Equals("Ó¼"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "³È");
            }
            else if (potion.Name.Equals("ÑªÈâ×£¸£"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "×Ï");
            }
            else if (potion.Name.Equals("È¼ÉÕÆ¿"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "×Ï");
            }
            else if (potion.Name.Equals("ÖÏÏ¢"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "×Ï");
            }
            else if (potion.Name.Equals("ºÚ°µÖ®ÐÄ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "×Ï");
            }
            else if (potion.Name.Equals("ÅòÕÍ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "À¶");
            }
            else if (potion.Name.Equals("»ðÑæ¾»»¯"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "À¶");
            }
            else if (potion.Name.Equals("²£Á§´óÅÚ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "À¶");
            }
            else if (potion.Name.Equals("³¤±Ûºï"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "À¶");
            }
            else if (potion.Name.Equals("ÊµÑéÌå1ºÅ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ÂÌ");
            }
            else if (potion.Name.Equals("ÊµÑéÌå2ºÅ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ÂÌ");
            }
            else if (potion.Name.Equals("ÊµÑéÌå3ºÅ"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ÂÌ");
            }
            else if (potion.Name.Equals("Çà²ÝÒ©¼Á"))
            {
                sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ÂÌ");
            }
            else
            {
                sprite= Resources.Load<Sprite>(UIImagePath.ImagePath + "ÂÌ");
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
