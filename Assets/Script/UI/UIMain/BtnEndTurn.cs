using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.System;

public class BtnEndTurn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => EventSystem.Send<PlayerTurnEndTrigger>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
