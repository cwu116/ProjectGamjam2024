using Buff;
using Buff.Tool;
using Game;
using Game.Model;
using System.Collections;
using System.Collections.Generic;
using Buff.Config;
using DG.Tweening;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

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
        if (damage < 0)
        {
            Hp.AddValue(-damage);
        }
        else
        {
            int realDamage = damage - this.Defence;
            if (realDamage > 0)
            {
                Hp.AddValue(-realDamage);
                Debug.LogError(Hp);
                if (this.Hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    public void RefreshHpInUI()
    {
        Debug.Log("MaxHp: " + MaxHp + ",Hp: " + Hp);
        if (MaxHp < Hp)
        {
            if (MaxHp < 1)
            {
                MaxHp.AddValue(1, true);
            }
            Hp.AddValue(MaxHp, true);
        }
        if (IsPlayer)
        {
            UIMain.Instance.RefreshPlayerHp();
        }
        else
        {
            HorizontalLayoutGroup hpBar = transform.Find("HpEnemy").GetComponent<HorizontalLayoutGroup>();
            foreach (Transform child in hpBar.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < Hp; i++)
            {
               Heart HeartUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Heart"), hpBar.transform).GetComponent<Heart>();
               HeartUI.isPlayer = false;
               // HeartUI.GetComponent<RectTransform>().localScale = new Vector3(0.055f,0.055f,1);
               HeartUI.gameObject.GetComponent<RectTransform>().rect.Set(2.3134f,2.3134f,2.3134f, 2.3134f);
               HeartUI.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2.3134f, 2.3134f);
            }
        }
    }

    public virtual void Die()
    {
        isDead = true;
        EventSystem.Send<EnemyDieEvent>(new EnemyDieEvent { enemy = this }); 
    }

    public virtual void Hatred()
    {
    }

    public void Away(params Param[] paramList)
    {
        // paramList[0] : 范围
        // paramList[1] : 移动距离
        // paramList[2] : 对象
        Debug.Log("Away");
        List<HexCell> AllCell = new List<HexCell>(GameBody.GetSystem<MapSystem>()
            .GetRoundHexCell(_curHexCell.Pos, paramList[0].ToInt()));
        List<GameObject> entities = new List<GameObject>();
        foreach (var cell in AllCell)
        {
            if (cell.gameObject != null)
            {
                entities.Add(cell.gameObject);
            }
        }

        // 遍历每一个附近个体，让它们退却
        foreach (var entity in entities)
        {
            List<HexCell> CellUnits = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                .GetRoundHexCell(_curHexCell.Pos, paramList[1].ToInt()));
            HexCell target = null;
            foreach (var cell in CellUnits)
            {
                if (Vector3.Cross(transform.forward, transform.position).y *
                    Vector3.Cross(transform.forward, entity.transform.position).y < 0)
                {
                    target = cell;
                    break;
                }
            }
            entity.transform.DOMove(target.transform.position, 0.5f);
        }
    }

    public void SpawnPath(params Param[] paramList)
    {
        // paramList[0] : 类型
        // paramList[1] : 持续回合
        spawningPath = string.Format("{0}*{1}", paramList[0].ToInt(), paramList[1].ToInt());
    }

    public void Sleep(params Param[] paramList)
    {
        // paramList[0] : 开启(1)关闭(0)
        if (paramList[0])
        {
            StateSystem.Execution(new List<string>() {"State:Sleep,3,true"}, gameObject);
        }
        else
        {
            buff.RemoveState(buff.GetUnitFromID("Sleep"));
            MoveTimes.RemoveChange();
        }
    }
}