using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.System;

public class BtnAddMat : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => EventSystem.Send<AddItemEvent>(new AddItemEvent() { item = new Item_s("blue", "黄色草药", "aaaa", 0), count = 3 }));
    }
}
