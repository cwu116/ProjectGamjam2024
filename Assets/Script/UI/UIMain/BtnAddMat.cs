using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.System;

public class BtnAddMat : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => EventSystem.Send<AddItemEvent>(new AddItemEvent() { item = new Item_s("yellow", "»ÆÉ«²ÝÒ©", "aaaa", 0), count = 3 }));
    }
}
