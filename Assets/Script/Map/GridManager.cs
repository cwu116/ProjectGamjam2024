using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using Game.Util;
using System.IO;
using Game;
using Game.System;
using UnityEditor.Timeline;

[DefaultExecutionOrder(-1)]
public class GridManager : MonoSingleton<GridManager>
{

    Mapdata mapData;
    MapSystem mapSystem;
    GameObject[] prefabMapList;
    GameObject[] prefabEnmeyList;
    HexCell[,] cells;

    List<BaseEntity> enemys;
    int height;
    int width;

    public HexCell[,] hexCells
    {
        get
        {
            return cells;
        }
        private set
        {

        }
    }

    public List<BaseEntity> Enemys
    {
        get
        {
            return enemys;
        }
        private set
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mapSystem = GameBody.GetSystem<MapSystem>();
        mapData = mapSystem.LoadMap();
        width = mapData.width;
        height = mapData.height;
        prefabMapList = mapSystem.GetHexCells();
        prefabEnmeyList = mapSystem.GetEnemies();
        CreateCells();
        CreateEnmey();
        mapSystem.InitHW();
        GameBody.GetSystem<MapSystem>().InitHW();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateCells()
    {
        cells = new HexCell[height, width];
        for (int y = 0, i = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                int index = x + y * height;
                CreateOneCell(x, y, i++, mapData.cells[index]);
            }
        }
    }

    HexCell CreateOneCell(int x, int y, int i, HexType type)
    {
        Vector3 position;
        position.y = (x + y * 0.5f - y / 2) * (Hex.innerRadius * 2f);
        position.x = y * Hex.outerRadius * 1.5f;
        position.z = 0;
        for (int j = 0; j < prefabMapList.Length; j++)
        {
            if (prefabMapList[j].GetComponent<HexCell>().Type == type)
            {
                cells[x, y] = Instantiate(prefabMapList[j]).GetComponent<HexCell>();
                break;
            }
        }
        HexCell cell = cells[x, y];
        cell.WidthIndex = y;
        cell.HeightIndex = x;
        cell.Pos = new Vector2(x, y);
        cell.name = y.ToString() + " " + x.ToString();
        Canvas canvas = cell.GetComponentInChildren<Canvas>();
        TMP_Text text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = x.ToString() + "\n" + y.ToString();
        GameObject gridmanager = GameObject.Find("GridManager");
        cell.transform.SetParent(gridmanager.transform, false);
        cell.transform.localPosition = position;
        cell.transform.rotation = Quaternion.Euler(0, 0, 120);
        return cell;
    }

    void CreateEnmey()
    {
        enemys = new List<BaseEntity>();
        for (int i = 0; i < mapData.enemyNames.Length; i++)
        {
            string name = mapData.enemyNames[i];
            int x = mapData.enemyposx[i];
            int y = mapData.enemyposy[i];
            for (int j = 0; j < prefabEnmeyList.Length; j++)
            {
                if (prefabEnmeyList[j].name == name)
                {
                    BaseEntity enemy = Instantiate(prefabEnmeyList[j]).GetComponent<BaseEntity>();
                    enemy.CurHexCell = cells[x, y];
                    cells[x, y].OccupyObject = enemy.gameObject;
                    enemy.CurrentHeightIndex = x;
                    enemy.CurrentWidthIndex = y;
                    enemy.SpawnPoint = enemy.CurHexCell.Pos;
                    if (enemy is Player)
                        enemy.transform.position = cells[x, y].transform.position + Vector3.up * 3;
                    else
                        enemy.transform.position = cells[x, y].transform.position;
                    enemys.Add(enemy);
                    GameObject EnemyParent = GameObject.Find("Enemys");
                    enemy.transform.SetParent(EnemyParent.transform, false);
                    break;
                }
            }
        }
    }

    public void ChangeHexCell(HexCell deleteCell, HexType type)
    {
        int heightIndex = deleteCell.HeightIndex;
        int widthIndex = deleteCell.WidthIndex;
        Destroy(cells[deleteCell.HeightIndex, deleteCell.WidthIndex].gameObject);
        cells[heightIndex, widthIndex] = null;
        cells[heightIndex, widthIndex] = CreateOneCell(heightIndex, widthIndex, 0, type);
    }
}

