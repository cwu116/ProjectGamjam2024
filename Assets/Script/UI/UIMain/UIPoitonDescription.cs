using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.System;

public class UIPoitonDescription : MonoBehaviour
{
    TextMeshProUGUI potionName;
    TextMeshProUGUI potionDescription;
    Image image;
    private void Awake()
    {
        potionName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        potionDescription = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        image = transform.Find("Icon").GetComponent<Image>();
    }

    public void Show(Item_Data potion,Sprite sprite)
    {
        gameObject.SetActive(true);
        potionName.text = potion.Name;
        potionDescription.text = potion.Description;
        image.sprite = sprite;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
