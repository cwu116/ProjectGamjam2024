using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.System;

public class UIRecipeElement : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public void OnPointerClick(PointerEventData eventData)
    {
        Game.System.EventSystem.Send(new UIRecipeElementClickEvent() { element = this });
    }
}
