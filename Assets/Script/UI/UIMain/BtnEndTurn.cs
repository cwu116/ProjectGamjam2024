using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.System;

public class BtnEndTurn : MonoBehaviour
{
    [SerializeField] private bool isForPlayer;
    void Start()
    {
        if (isForPlayer)
        {
            this.GetComponent<Button>().onClick.AddListener(() => EventSystem.Send<PlayerTurnEndTrigger>());
        }
        else
        {
            this.GetComponent<Button>().onClick.AddListener(() => EventSystem.Send<EnemyTurnEndTrigger>());
        }
    }

    void Update()
    {
        
    }
}
