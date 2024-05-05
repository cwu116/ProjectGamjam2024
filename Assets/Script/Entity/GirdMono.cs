using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;
using UnityEngine.EventSystems;
using Game.Model;
using Game;

public class GirdMono : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameBody.GetModel<TurnModel>().CurrentTurn != TurnType.PlayerTurn)
            return;
        if (GameBody.GetModel<PlayerActionModel>().currentPotion != null)
            return;

        Game.System.EventSystem.Send<GirdCilckEvent>(new GirdCilckEvent { cell = this.GetComponent<HexCell>(), transfrom = this.transform });
    }
}
