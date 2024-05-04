using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;
using UnityEngine.EventSystems;

public class GirdMono : MonoBehaviour
{
    private void OnMouseDown()
    {
        Game.System.EventSystem.Send<GirdCilckEvent>(new GirdCilckEvent { cell = this.GetComponent<HexCell>(), transfrom = this.transform });
        Debug.Log("Gird Click");
    }
}
