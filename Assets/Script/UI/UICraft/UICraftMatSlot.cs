using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Managers;

namespace Game.UI
{
    public class UICraftMatSlot : MonoBehaviour, IPointerClickHandler
    {
        public Item item;
        TextMeshProUGUI countText;
        TextMeshProUGUI matName;
        Image icon;
        Transform flask;
        private void Start()
        {
            icon =transform.Find("Icon").GetComponent<Image>();
            countText = transform.Find("Count").GetComponent<TextMeshProUGUI>();
            matName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            //flask = UIManager.Get<UICraft>().transform.Find("Flask");

            countText.text = "10";
        }

        public void Refresh(/*Item item*/)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //sendEvent(Item)
        }
    }
}

