using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

public class CustomStyleButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
{
    Vector3 originalRotation;
    Vector3 originalScale;

    private void Awake()
    {
        originalRotation = transform.rotation.eulerAngles;
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(originalScale * 0.8f, 0.2f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(originalScale * 1.2f, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * 1.2f, 0.2f);
        transform.DOShakeRotation(0.4f, new Vector3(0, 0, 60), 10, 60,true,ShakeRandomnessMode.Harmonic).SetEase(Ease.OutQuart);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, 0.2f);
        transform.DORotate(originalRotation, 0.2f);
    }


}
