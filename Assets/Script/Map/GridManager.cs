using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class GridManager : MonoBehaviour
{

    Mapdata mapData;
    public List<GameObject> prefabList;
    HexCell[,] cells;
    int height;
    int width;

    // Start is called before the first frame update
    void Start()
    {
        mapData = AssetDatabase.LoadAssetAtPath<Mapdata>(@"Assets/ScriptableObject/map1.asset");
        width = mapData.width;
        height = mapData.height;
        CreateCells();
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
        for (int j = 0; j < prefabList.Count; j++)
        {
            if (prefabList[j].GetComponent<HexCell>().Type == type)
            {
                cells[x, y] = Instantiate(prefabList[j]).GetComponent<HexCell>();
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
}