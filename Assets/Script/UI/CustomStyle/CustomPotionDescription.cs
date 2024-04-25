using UnityEngine;
using DG.Tweening;

public class CustomPotionDescription : MonoBehaviour
{
    public Vector3 originalPos;
    public Vector3 targetPos;
    private void OnEnable()
    {
        transform.position = originalPos;
        transform.DOMove(targetPos, 0.2f).SetEase(Ease.OutQuart);
    }

    [ContextMenu("CheckPostion")]
    private void Check()
    {
        Debug.Log(transform.position);
    }
}
