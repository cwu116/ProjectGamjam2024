using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public bool isPlayer;
    public Image heartIcon;
    
    private void Awake()
    {
        heartIcon = GetComponent<Image>();
    }

    void Start()
    {
        if (isPlayer)
        {
            heartIcon.sprite = Resources.Load<Sprite>("Sprites/主角血量");
        }
        else
        {
            heartIcon.sprite = Resources.Load<Sprite>("Sprites/怪物血");
        }
        
    }

    void Update()
    {
        
    }
}
