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
            
            List<Sprite> sprites = new List<Sprite>();
            //sprites.Add(Resources.Load<Sprite>(UIImagePath.ImagePath + "ºì"));
            sprites.Add(Resources.Load<Sprite>(UIImagePath.ImagePath + "ÂÌ"));
            //sprites.Add(Resources.Load<Sprite>(UIImagePath.ImagePath + "À¶"));
            //sprites.Add(Resources.Load<Sprite>(UIImagePath.ImagePath + "·Û"));
            //sprites.Add(Resources.Load<Sprite>(UIImagePath.ImagePath + "×Ï"));
            icon.sprite = sprites[Random.Range(0, sprites.Count)];

            this.potion = potion;
            this.owner = owner;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameBody.GetModel<PlayerActionModel>().CurrentPotion != this.potion)
            {
                Game.System.EventSystem.Send<OnPotionClick>(new OnPotionClick() { potion = potion });
            }
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
