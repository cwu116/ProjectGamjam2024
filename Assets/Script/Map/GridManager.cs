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

public class GridManager : MonoSingleton<GridManager>
{

    Mapdata mapData;
    MapSystem mapSystem;
    GameObject[] prefabMapList;
    GameObject[] prefabEnmeyList;
    HexCell[,] cells;

    List<Enemy> enemys;
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

    public List<Enemy> Enemys
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

    void CreateOneCell(int x, int y, int i, HexType type)
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

        cell.name = y.ToString() + " " + x.ToString();
        Canvas canvas = cell.GetComponentInChildren<Canvas>();
        TMP_Text text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = x.ToString() + "\n" + y.ToString();
        GameObject gridmanager = GameObject.Find("GridManager");
        cell.transform.SetParent(gridmanager.transform, false);
        cell.transform.localPosition = position;
    }

    void CreateEnmey()
    {
        enemys = new List<Enemy>();
        for (int i = 0; i < mapData.enemyNames.Length; i++)
        {
            string name = mapData.enemyNames[i];
            int x = mapData.enemyposx[i];
            int y = mapData.enemyposy[i];
            for (int j = 0; j < prefabEnmeyList.Length; j++)
            {
                if (prefabEnmeyList[j].name == name)
                {
                    Enemy enemy = Instantiate(prefabEnmeyList[j]).GetComponent<Enemy>();
                    enemy.CurHexCell = cells[x, y];
                    enemy.CurrentHeightIndex = x;
                    enemy.CurrentWidthIndex = y;
                    enemy.transform.position = cells[x, y].transform.position;
                    enemys.Add(enemy);
                    GameObject EnemyParent = GameObject.Find("Enemys");
                    enemy.transform.SetParent(EnemyParent.transform, false);
                    break;
                }
            }
        }
    }
}

