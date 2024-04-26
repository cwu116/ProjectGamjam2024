using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

namespace Game.UI
{
    public class UICraftElement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        Image icon;

        public void OnPointerClick(PointerEventData eventData)
        {
            //ªÿ ’≤ƒ¡œ
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, 0.2f);
        }

        public void Init(/*Item item*/)
        {

        }

        private void Start()
        {
            icon = GetComponent<Image>();
        }
    }
}


