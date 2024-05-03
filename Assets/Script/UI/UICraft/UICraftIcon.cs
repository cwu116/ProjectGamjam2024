using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.System;
using UnityEngine.UI;

public class UICraftIcon : MonoBehaviour, IPointerClickHandler
{
    public bool isSpecial;
    public Item item;
    public Image icon;

    private void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Game.System.EventSystem.Send(new UICraftIconClickEvent(this));
    }
}
