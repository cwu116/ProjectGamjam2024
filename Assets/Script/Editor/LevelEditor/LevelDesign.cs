using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using Game.Util;
using System.IO;
using DG.Tweening.Plugins;
using System.Drawing.Text;

public class LevelDesign : EditorWindow
{
    [MenuItem("Window/Level Design")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelDesign));
    }

    // 当前选择的预制体索引
    int selectedIndex = 0;

    // 用于存储找到的预制体名称
    private string[] mapPrefabNames;
    // 用于存储找到的预制体路径
    private string[] mapPrefabPaths;

    GameObject prefab = null;

    // 当前选择的预制体索引
    int enemySelectedIndex = 0;

    // 用于存储找到的预制体名称
    private string[] enemyPrefabNames;
    // 用于存储找到的预制体路径
    private string[] enemyPrefabPaths;

    GameObject enemyPrefab = null;



    string widthstr = "";
    string heightstr = "";
    int width = 0;
    int height = 0;
    HexCell[,] cells;
    List<BaseEntity> enemeys;
    void OnEnable()
    {
        // 查找所有预制体
        string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/Resources/Prefabs/MapHexCell" });
        mapPrefabPaths = new string[guids.Length];
        mapPrefabNames = new string[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            mapPrefabPaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
            mapPrefabNames[i] = System.IO.Path.GetFileNameWithoutExtension(mapPrefabPaths[i]);
        }
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>(mapPrefabPaths[0]);

        // 查找所有预制体
        string[] enemyGuids = AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/Resources/Prefabs/Enemy" });
        enemyPrefabPaths = new string[enemyGuids.Length];
        enemyPrefabNames = new string[enemyGuids.Length];

        for (int i = 0; i < enemyGuids.Length; i++)
        {
            enemyPrefabPaths[i] = AssetDatabase.GUIDToAssetPath(enemyGuids[i]);
            enemyPrefabNames[i] = System.IO.Path.GetFileNameWithoutExtension(enemyPrefabPaths[i]);
        }
        enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(enemyPrefabPaths[0]);

        enemeys = new List<BaseEntity>();
    }

    void OnGUI()
    {
        GUILayout.Label("Select a Prefab", EditorStyles.boldLabel);

        //选择预制体
        ChoosePrefab(mapPrefabPaths, mapPrefabNames, selectedIndex, out int index1, 0);
        selectedIndex = index1;
        //选择地图宽度和高度
        SelectWidthAndHeight();



        if (GUILayout.Button("创建地图"))
        {
            if (prefab != null)
            {
                CreateCells();
            }
            else
            {
                Debug.LogError("空");
            }
        }
        if (GUILayout.Button("删除地图"))
        {
            GameObject gridManager = GameObject.Find("GridManager");
            if (gridManager != null)
            {
                // 创建一个列表来存储所有要删除的子对象
                List<GameObject> children = new List<GameObject>();

                // 首先收集所有子对象
                foreach (Transform child in gridManager.transform)
                {
                    children.Add(child.gameObject);
                }

                // 然后在另一个循环中删除它们
                foreach (GameObject child in children)
                {
                    DestroyImmediate(child);
                }
            }
        }
        // //选择预制体
        // prefab = ChoosePrefab(mapPrefabPaths, mapPrefabNames, selectedIndex, out int index2);
        // selectedIndex = index2;
        if (GUILayout.Button("替换地块"))
        {
            if (UnityEditor.Selection.transforms.Length == 0)
            {
                Debug.LogError("请选中一个地块");
                return;
            }
            if (UnityEditor.Selection.transforms[0].parent.name != "GridManager")
            {
                Debug.LogError("请选中一个地块");
                return;
            }
            HexCell deleteObj = UnityEditor.Selection.transforms[0].GetComponent<HexCell>();
            string name = deleteObj.name;
            Vector3 pos = deleteObj.transform.position;
            Quaternion rot = deleteObj.transform.rotation;
            int x = deleteObj.HeightIndex;
            int y = deleteObj.WidthIndex;
            int index = deleteObj.transform.GetSiblingIndex();
            DestroyImmediate(deleteObj.gameObject);
            GameObject gridmanager = GameObject.Find("GridManager");
            HexCell cell = Instantiate(prefab, pos, rot, gridmanager.transform).GetComponent<HexCell>();
            cells[x, y] = cell;
            cell.gameObject.name = name;
            cell.WidthIndex = y;
            cell.HeightIndex = x;
            Canvas canvas = cell.GetComponentInChildren<Canvas>();
            TMP_Text text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
            text.text = x.ToString() + "\n" + y.ToString();
            cell.transform.SetSiblingIndex(index);
        }

        //选择敌人预制体
        ChoosePrefab(enemyPrefabPaths, enemyPrefabNames, enemySelectedIndex, out int index3, 1);
        enemySelectedIndex = index3;

        if (GUILayout.Button("创建敌人"))
        {
            if (UnityEditor.Selection.transforms.Length == 0)
            {
                Debug.LogError("请选中一个地块");
                return;
            }
            //判断如果选中物体的父物体是不是gridmanager
            if (UnityEditor.Selection.transforms[0].parent.name != "GridManager")
            {
                Debug.LogError("请选中一个地块");
                return;
            }
            GameObject enemyPrefabs = GameObject.Find("Enemys");
            BaseEntity enemy = Instantiate(enemyPrefab, enemyPrefabs.transform).GetComponent<BaseEntity>();
            enemy.transform.position = UnityEditor.Selection.transforms[0].position;
            enemy.gameObject.name = enemyPrefab.name;
            enemy.CurrentHeightIndex = UnityEditor.Selection.transforms[0].GetComponent<HexCell>().HeightIndex;
            enemy.CurrentWidthIndex = UnityEditor.Selection.transforms[0].GetComponent<HexCell>().WidthIndex;
            enemeys.Add(enemy);
        }

        if (GUILayout.Button("删除敌人"))
        {
            enemeys.Clear();
            GameObject enemyPrefabs = GameObject.Find("Enemys");
            if (enemyPrefabs != null)
            {
                // 创建一个列表来存储所有要删除的子对象
                List<GameObject> children = new List<GameObject>();

                // 首先收集所有子对象
                foreach (Transform child in enemyPrefabs.transform)
                {
                    children.Add(child.gameObject);
                }

                // 然后在另一个循环中删除它们
                foreach (GameObject child in children)
                {
                    DestroyImmediate(child);
                }
            }
        }

        if (GUILayout.Button("保存地图1"))
        {


            File.WriteAllText("Assets/Resources/MapData/MapData1.json", JsonUtil.ToJson(SaveMap()));
        }

        if (GUILayout.Button("保存地图2"))
        {

            SaveMap();
            File.WriteAllText("Assets/Resources/MapData/MapData2.json", JsonUtil.ToJson(SaveMap()));
        }

        if (GUILayout.Button("保存地图3"))
        {

            SaveMap();
            File.WriteAllText("Assets/Resources/MapData/MapData3.json", JsonUtil.ToJson(SaveMap()));
        }

        if (GUILayout.Button("保存地图4"))
        {

            SaveMap();
            File.WriteAllText("Assets/Resources/MapData/MapData4.json", JsonUtil.ToJson(SaveMap()));
        }

        if (GUILayout.Button("保存地图5"))
        {

            SaveMap();
            File.WriteAllText("Assets/Resources/MapData/MapData5.json", JsonUtil.ToJson(SaveMap()));
        }

    }



    void CreateCells()
    {
        cells = new HexCell[height, width];
        for (int y = 0, i = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                CreateOneCell(x, y, i++);
            }
        }
    }
    Mapdata SaveMap()
    {
        Mapdata map = new Mapdata();
        map.height = cells.GetLength(0);
        map.width = cells.GetLength(1);
        map.cells = new HexType[map.height * map.width];
        map.enemyNames = new string[enemeys.Count];
        map.enemyposx = new int[enemeys.Count];
        map.enemyposy = new int[enemeys.Count];

        for (int i = 0; i < enemeys.Count; i++)
        {
            map.enemyNames[i] = enemeys[i].name;
            map.enemyposx[i] = enemeys[i].CurrentHeightIndex;
            map.enemyposy[i] = enemeys[i].CurrentWidthIndex;
        }

        for (int y = 0, i = 0; y < map.width; y++)
        {
            for (int x = 0; x < map.height; x++)
            {
                map.cells[i++] = cells[x, y].Type;
            }
        }
        return map;
    }
    void CreateOneCell(int x, int y, int i)
    {
        Vector3 position;
        position.y = (x + y * 0.5f - y / 2) * (Hex.innerRadius * 2f);
        position.x = y * Hex.outerRadius * 1.5f;
        position.z = 0;
        HexCell cell = cells[x, y] = Instantiate(prefab).GetComponent<HexCell>();
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

    void ChoosePrefab(string[] PrefabPaths, string[] PrefabNames, int index, out int index1, int type)
    {
        EditorGUI.BeginChangeCheck();
        index1 = EditorGUILayout.Popup("Prefab", index, PrefabNames);
        if (EditorGUI.EndChangeCheck())
        {
            // 当选择变化时的逻辑，例如加载并显示选中的预制体
            Debug.Log($"Selected prefab: {PrefabNames[index1]} at path: {PrefabPaths[index1]}");

            // 示例：在场景中实例化选中的预制体

            if (type == 0)
                prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPaths[index1]);
            else
                enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPaths[index1]);
            //PrefabUtility.InstantiatePrefab(prefab);
        }
    }

    void SelectWidthAndHeight()
    {
        widthstr = EditorGUILayout.TextField("地图宽度", widthstr);
        heightstr = EditorGUILayout.TextField("地图高度", heightstr);
        if (widthstr != "" && heightstr != "")
        {
            width = int.Parse(widthstr);
            height = int.Parse(heightstr);
        }
    }



}