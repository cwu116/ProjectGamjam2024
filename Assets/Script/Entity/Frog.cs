using System;
using System.Collections;
using System.Collections.Generic;
using Buff;
using Buff.Config;
using Game;
using Game.System;
using UnityEngine;

public class Frog : Enemy
{
    public enum FrogType
    {
        TrumpetFrog,        // 喇叭蛙
        BlackMistFrog,      // 黑雾蛙
        BombFrog            // 爆炸蛙
    }
    [SerializeField] private Enemy spawnEnemy;
    [SerializeField] public FrogType frogType;
    private void Awake()
    {
        buff = GetComponent<BuffComponent>();
        IsPlayer = false;
        
        buff.RegisterFunc(ActionKey.Die, Die);
        buff.RegisterFunc(TActionKey.Away, Away);
        buff.RegisterFunc(TActionKey.SpawnPath, SpawnPath);
        buff.RegisterFunc(TActionKey.Sleep, Sleep);
    }

    void Start()
    {
        SetModel(name);
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Instantiate(spawnEnemy, transform.position, Quaternion.identity);
    }

    // 受到攻击时
    void OnDamage()
    {
        switch (frogType)
        {
            case FrogType.TrumpetFrog: 
                // 惊动
                break;
            case FrogType.BlackMistFrog:
                StateSystem.Execution(new List<string>(){"Create:true,BlackMist,1"}, gameObject);
                break;
            case FrogType.BombFrog: 
                List<HexCell> cellList = new List<HexCell>(GameBody.GetSystem<MapSystem>()
                    .GetRoundHexCell(_curHexCell.Pos, 1));
                foreach (var cell in cellList)
                {
                    if (cell.OccupyObject is not null)
                    {
                        StateSystem.Execution(new List<string>(){"ChangeValue:Damage,1"}, cell.OccupyObject);
                    }
                }
                break;
        }
    }
}
