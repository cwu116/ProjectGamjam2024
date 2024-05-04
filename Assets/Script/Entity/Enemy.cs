using System;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using UnityEngine;

public class Enemy : BaseEntity
{

    [SerializeField] private new string name;
    private void Awake()
    {
        buff = GetComponent<BuffComponent>();
        IsPlayer = false;
    }

    private void Start()
    {
        SetModel(name);
        
        base.Start();
    }

}
