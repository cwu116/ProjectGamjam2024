using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.System;

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
            icon.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + potion.id);
            this.potion = potion;
            this.owner = owner;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(owner.currentPotion!=this.potion)
            {
                owner.currentPotion = this.potion;
                Game.System.EventSystem.Send<OnPotionClick>(new OnPotionClick() { potion = potion });
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            owner.descriptionUI.Show(this.potion);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            owner.descriptionUI.Close();
        }
    }
}
