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

    
    private void Awake()
    {
        buffComp = GetComponent<BuffComponent>();

        // 初始化变量
        Hp = new ValueInt(3);

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
