using System;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Tool;
using Game.System;
using UnityEngine;

public class PlayTemplate : MonoBehaviour
{
    private BuffComponent buffComp;

    public BuffComponent BuffComp
    {
        get { return buffComp; }
    }

    private int Hp;
    // ValueInt 声明举例 ： 在set里注册组件变量完就行，不需要在其他地方再注册了
    public ValueInt MaxHp
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey("MaxHp"))
            {
                return comp.ValueUnits["MaxHp"];
            }
            return new ValueInt(0);
        }
        set 
        {             
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey("MaxHp"))
            {
                comp.ValueUnits["MaxHp"] = value;
            }
            else
            {
                comp.RegisterParam("MaxHp", value);
            }
        }
    }
    public ValueInt MoveRange
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey("MoveRange"))
            {
                return comp.ValueUnits["MoveRange"];
            }
            return new ValueInt(0);
        }
        set 
        {             
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey("MoveRange"))
            {
                comp.ValueUnits["MoveRange"] = value;
            }
            else
            {
                comp.RegisterParam("MoveRange", value);
            }
        }
    }
    public ValueInt MoveForce
    {
        get
        {
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey("MoveForce"))
            {
                return comp.ValueUnits["MoveForce"];
            }
            return new ValueInt(0);
        }
        set 
        {             
            BuffComponent comp = GetComponent<BuffComponent>();
            if (comp.ValueUnits.ContainsKey("MoveForce"))
            {
                comp.ValueUnits["MoveForce"] = value;
            }
            else
            {
                comp.RegisterParam("MoveForce", value);
            }
        }
    }
    
    private void Awake()
    {
        buffComp = GetComponent<BuffComponent>();

        // 初始化变量
        Hp = new ValueInt(3);
        MaxHp = new ValueInt(3);
        MoveRange = new ValueInt(2);
        MoveForce = new ValueInt(1);

    }

    void Update()
    {
        
    }

    public ValueInt ShowParam(int index)
    {
        switch (index)
        {
            // case 0: return BuffComp.Get("HP");
            // case 1: return BuffComp.Get("MaxHP");
            // case 2: return BuffComp.Get("MoveRange");
            // case 3: return BuffComp.Get("MoveForce");
            
            case 0: return new ValueInt(Hp);
            case 1: return BuffComp.Get("MaxHP");
            case 2: return BuffComp.Get("MoveRange");
            case 3: return BuffComp.Get("MoveForce");
        }
        return new ValueInt(0);
    }
}
