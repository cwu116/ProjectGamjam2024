using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.System;
using System;

public class BtnEndTurn : MonoBehaviour
{
    [SerializeField] private bool isForPlayer;
    void Start()
    {
        if (isForPlayer)
        {
            this.GetComponent<Button>().onClick.AddListener(EndPlayerTurn);
        }
        else
        {
            this.GetComponent<Button>().onClick.AddListener(() => EventSystem.Send<EnemyTurnEndTrigger>());
        }
    }

    private void EndPlayerTurn()
    {
        EventSystem.Send<PlayerTurnEndTrigger>();
    }

    void Update()
    {
        
    }
}
