using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Model;
using Game.System;
using UnityEditor;
using UnityEngine;

namespace Game.System
{
    public partial class MapSystem : BaseSystem
    {
        private MapModel _mapModel;
        private GameObject[] hexCells;

        private GameObject[] enemies;
        public override void InitSystem()
        {
            _mapModel = GameBody.GetModel<MapModel>();
            Console.WriteLine("MapSystem Init");
            LoadPrefabs();
            //InitHW();
        }

        public Mapdata LoadMap()
        {
            Mapdata mapdata = _mapModel.MapDatas;
            return mapdata;
        }

        public void LoadPrefabs()
        {
            hexCells = _mapModel.hexCells;
            enemies = _mapModel.enemies;
        }

        public GameObject[] GetHexCells()
        {
            return hexCells;
        }

        public GameObject[] GetEnemies()
        {
            return enemies;
        }

    }
}
