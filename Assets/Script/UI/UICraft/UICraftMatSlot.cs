using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Managers;
using Game.System;

namespace Game.UI
{
    public class UICraftMatSlot : MonoBehaviour, IPointerClickHandler
    {
        public Item_s item;
        public TextMeshProUGUI countText;
        public TextMeshProUGUI matName;
        public Image icon;
        Transform flask;
        private void Awake()
        {
            icon =transform.Find("Icon").GetComponent<Image>();
            countText = transform.Find("Count").GetComponent<TextMeshProUGUI>();
            matName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            //flask = UIManager.Get<UICraft>().transform.Find("Flask");

            countText.text = "10";
        }

        public void SetActive(bool active)
        {
            icon.gameObject.SetActive(active);
            countText.gameObject.SetActive(active);
            matName.gameObject.SetActive(active);
        }

        public void Refresh(Item_s item)
        {
            this.item = item;
            icon.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + this.item.Id);
            countText.text = "x"+this.item.quantity.ToString();
            matName.text = this.item.Name;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Game.System.EventSystem.Send<UICraftMaterialClickEvent>(new UICraftMaterialClickEvent() { item = this.item });
        }
    }
}

