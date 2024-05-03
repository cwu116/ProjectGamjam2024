using Buff;
using Buff.Tool;
using Game;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff.Config;
using Game.System;
using UnityEngine;

public partial class BaseEntity : MonoBehaviour
{
    public bool SetModel(string name)
    {
        AttackUnit_Data ad = GameBody.GetModel<AttackUnitModel>().GetDataByName(name);
        if (ad != null)
        {
            this._model = ad;
            return true;
        }

        return false;
    }

    public bool CanMove()
    {
        if (this.MoveTimes <= 0)
            return false;

        return true;
    }

    public virtual void UseSkill(BaseEntity target)
    {
        MoveTimes.AddValue(-1);
    }

    public void GetHurt(int damage)
    {
        int realDamage = damage - this.Defence;
        if (realDamage > 0)
        {
            Hp.AddValue(realDamage);
            if (this.Hp <= 0)
            {
                Die();
            }

            return;
        }
    }
    
    public virtual void Die() { }
    
    public virtual void Hatred() { }

    public void Away(params Param[] paramList)
    {
        // paramList[0] : 范围
        // paramList[1] : 移动距离
    }
    
    public void SpawnPath(params Param[] paramList)
    {
        // paramList[0] : 类型(1:火焰Fire  2:黑雾:BlackMist)
        // paramList[1] : 持续回合
    }
    
    public void Sleep(params Param[] paramList)
    {
        // paramList[0] : 开启(1)关闭(0)
        if (paramList[0])
        {
            StateSystem.Execution(new List<string>(){ "State:Sleep,3,true" }, gameObject);
        }
        else
        {
            buff.RemoveState(buff.GetUnitFromID("Sleep"));
            MoveTimes.RemoveChange();
        }
    }
}