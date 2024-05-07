using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Script.Config;
using Game.System;
using Game.Util;
using MainLogic.Manager;
//using UnityEditor;
using UnityEngine;
using System.IO;

namespace Game.Model
{
    public class MapModel : BaseModel
    {
        public Mapdata MapDatas;
        public GameObject[] hexCells;

        public GameObject[] enemies;
        public override void InitModel()
        {
            MapDatas = JsonUtil.ToObject<Mapdata>(
                ResourcesManager.LoadText(JsonPath.MapPath, JsonFileName.GameMapName));

            //hexCells = LoadPrefab("Assets/Resources/Prefabs/MapHexCell");
            hexCells = Resources.LoadAll<GameObject>("Prefabs/MapHexCell");
            //enemies = LoadPrefab("Assets/Resources/Prefabs/Enemy");
            enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy");
        }


        //GameObject[] LoadPrefab(string path)
        //{
        //    // 查找所有预制体
        //    string[] Guids = AssetDatabase.FindAssets("t:GameObject", new[] { path });
        //    string[] PrefabPaths = new string[Guids.Length];
        //    string[] PrefabNames = new string[Guids.Length];
        //    GameObject[] Prefabs = new GameObject[Guids.Length];
        //    for (int i = 0; i < Guids.Length; i++)
        //    {
        //        PrefabPaths[i] = AssetDatabase.GUIDToAssetPath(Guids[i]);
        //        PrefabNames[i] = Path.GetFileNameWithoutExtension(PrefabPaths[i]);
        //        Prefabs[i] = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPaths[i]);
        //    }
        //    return Prefabs;
        //}

        public void changeMapData(string name)
        {
            MapDatas = JsonUtil.ToObject<Mapdata>(
                ResourcesManager.LoadText(JsonPath.MapPath, name));
        }
    }
}
