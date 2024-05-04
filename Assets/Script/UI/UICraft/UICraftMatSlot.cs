using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Managers;

namespace Game.UI
{
    public class UICraftMatSlot : MonoBehaviour, IPointerClickHandler
    {
        public Item_s item;
        TextMeshProUGUI countText;
        TextMeshProUGUI matName;
        public Image icon;
        Transform flask;
        private void Start()
        {
            icon =transform.Find("Icon").GetComponent<Image>();
            countText = transform.Find("Count").GetComponent<TextMeshProUGUI>();
            matName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            //flask = UIManager.Get<UICraft>().transform.Find("Flask");

            countText.text = "10";
        }

        public void Refresh(Item_s item)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //sendEvent(Item)
        }
    }
}

