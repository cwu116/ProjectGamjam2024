using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.System;
using TMPro;
using UnityEngine.UI;

public class UIRecipeElement : MonoBehaviour, IPointerClickHandler
{
    public Item_Data potion;
    TextMeshProUGUI recipeName;
    Image background;
    Sprite sprite;

    public void Init(Item_Data potion)
    {
        background = GetComponent<Image>();
        recipeName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        this.potion = potion;
        recipeName.text = this.potion.Name;
        //this.sprite = sprite;
        //image.sprite = sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Game.System.EventSystem.Send<UIRecipeElementClickEvent>(new UIRecipeElementClickEvent() { element = this });
    }

    public void Select()
    {
        background.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "选中框");
    }

    public void UnSelect()
    {
        background.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "未选中框");
    }
}
