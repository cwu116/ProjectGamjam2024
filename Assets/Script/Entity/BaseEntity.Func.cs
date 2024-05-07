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
using Managers;

public partial class BaseEntity : MonoBehaviour
{
    // 生成实体
    public static GameObject SpawnEntity(GameObject classObj, HexCell Pos)
    {
        Enemy entity = Instantiate(classObj, Pos.transform.position, Quaternion.identity).GetComponent<Enemy>();
        entity.CurHexCell = Pos;
        entity.SpawnPoint = Pos.Pos;
        Pos.OccupyObject = entity.gameObject;
        GameObject EnemyParent = GameObject.Find("Enemys");
        entity.transform.SetParent(EnemyParent.transform, false);
        return entity.gameObject;
    }
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

    public virtual void UseSkill(BaseEntity target)
    {
        MoveTimes.AddValue(-1);
    }

    public virtual async void GetHurt(int damage)
    {
        if (damage < 0)
        {
            Hp.AddValue(-damage);
        }
        else
        {
            if (bFlamePure)
            {
                Hp.AddValue(-damage);
            }
            else
            {
                int realDamage = damage - Defence;
                if (realDamage > 0)
                {
                    Hp.AddValue(-realDamage);
                    if (Hp <= 0)
                    {
                        Die();
                        return;
                    }
                }
            }
        }

        EventSystem.Send<EntityHurtEvent>(new EntityHurtEvent() { enetity = this });//玩家受伤动画
        if (this as Enemy)
        {
            await System.Threading.Tasks.Task.Delay(1000);
            if (this == null)
                return;
            if (this.GetComponent<Animator>())
                this.GetComponent<Animator>().SetTrigger("Hit");
            await System.Threading.Tasks.Task.Delay(300);
            AudioManager.PlaySound(AudioPath.Effect);
        }
    }

    public void RefreshHpInUI()
    {
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
               HeartUI.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2.3134f, 2.3134f);
            }
        }
        // Debug.Log("MaxHp: " + MaxHp + ",Hp: " + Hp);
    }

    [ContextMenu("Die")]
    public virtual void Die()
    {
        isDead = true;
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f);

        if (this is Enemy)
        {
            _curHexCell.OccupyObject = null;
            EventSystem.Send<EnemyDieEvent>(new EnemyDieEvent { enemy = this });
            GameObject dropPrefab = Resources.Load<GameObject>("Prefabs/DropItems/DropItem");
            GameObject go = GameObject.Instantiate(dropPrefab);
            go.transform.position = this.transform.position;
            go.GetComponent<DropItem>().Init((this as Enemy).dropedItemId, (this as Enemy).dropedItemName);
            Game.System.EventSystem.Send<ClearAttackBlockEvent>();
            Game.System.EventSystem.Send<ClearWarningBlockEvent>();
            CurHexCell.OccupyObject = null;
        }
        else if (this is Player)
        {
            EventSystem.Send<PlayerDieEvent>();
        }
       
    }
    

    public void Away(params Param[] paramList)
    {
        // paramList[0] : 范围
        // paramList[1] : 移动距离
        // paramList[2] : 对象
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
                if (Vector3.Cross(transform.forward, cell.transform.position).y *
                    Vector3.Cross(transform.forward, entity.transform.position).y < 0)
                {
                    target = cell;
                    break;
                }
                else if (Vector3.Dot(transform.forward, entity.transform.position) == 0)
                {
                    if ((entity.transform.position.y - cell.transform.position.y)*(entity.transform.position.y - transform.position.y) < 0)
                    {
                        target = cell;
                        break;
                    }
                }
            }
            if (target is not null) entity.transform.DOMove(target.transform.position, 0.5f);
        }
    }

    public void SetVisibility(params Param[] paramList)
    {
        // paramList[0] : 开启(1)关闭(0)
        if (paramList[0])
        {
            StateSystem.Execution(new List<string>() {"State:Invisible,1,true"}, gameObject);
            GetComponent<SpriteRenderer>().DOFade(0.3f, 0.3f);
        }
        else
        {
            GetComponent<SpriteRenderer>().DOFade(1f, 0.3f);
            bInvisible.RemoveChange();
        }
    }

    public void SpawnPath(params Param[] paramList)
    {
        // paramList[0] : 类型
        // paramList[1] : 持续回合
        spawningPath = string.Format("{0}*{1}", paramList[0].ToString(), paramList[1].ToInt());
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