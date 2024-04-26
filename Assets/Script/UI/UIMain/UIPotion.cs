using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class UIPotion : MonoBehaviour, IPointerClickHandler
    {
        Image icon;
        private void Start()
        {
            icon = GetComponent<Image>();
        }

        public void Init(/*Potion potion*/)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // π”√“©º¡

        }
    }
}
