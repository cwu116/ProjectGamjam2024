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
    }

    void Start()
    {
        if (SetModel(enemyName))
        {
            InitEntity();
        }
        base.Start();
        RefreshHpInUI();
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (spawnEnemy is not null)
        {
            SpawnEntity(spawnEnemy.gameObject, CurHexCell);
        }
    }

    // 受到攻击时
    void OnDamage()
    {
        switch (frogType)
        {
            case FrogType.TrumpetFrog:
                HexCell[] around = GameBody.GetSystem<MapSystem>().GetRoundHexCell(_curHexCell.Pos, 2);
                foreach (var cell in around)
                {
                    if (cell.OccupyObject is not null)
                    {
                        cell.OccupyObject.GetComponent<BaseEntity>().isDisturbed = true;
                    }
                }
                HateValue += 15;
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
