using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;
using UnityEngine.EventSystems;
using Game.Model;
using Game;

public class GirdMono : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (GameBody.GetModel<TurnModel>().CurrentTurn != TurnType.PlayerTurn)
            return;
        Game.System.EventSystem.Send<GirdCilckEvent>(new GirdCilckEvent { cell = this.GetComponent<HexCell>(), transfrom = this.transform });
    }
}
