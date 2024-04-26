using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Managers;

namespace Game.UI
{
    public class CraftSlot : MonoBehaviour, IPointerClickHandler
    {
        //public Item_s item;
        TextMeshProUGUI countText;
        Image icon;
        Transform flask;
        GameObject prefab;
        private void Start()
        {
            icon =transform.Find("slot").GetComponent<Image>();
            countText = transform.Find("Count").GetComponent<TextMeshProUGUI>();
            Debug.Log(countText);
            flask = UIManager.Get<UICraft>().transform.Find("Flask");
            prefab = Resources.Load<GameObject>("UICraftElement");

            countText.text = "10";
        }

        public void Refresh(/*Item item*/)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameObject obj = Instantiate(prefab, flask);
            obj.GetComponent<UICraftElement>().Init();
        }
    }
}

