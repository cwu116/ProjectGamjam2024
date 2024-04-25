using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Model;
using Game.System;
using UnityEditor;

namespace Game.System
{
    public partial class MapSystem : BaseSystem
    {
        private MapModel _mapModel;
        public override void InitSystem()
        {
            _mapModel = GameBody.GetModel<MapModel>();
            Console.WriteLine("MapSystem Init");
        }

        public Mapdata LoadMap()
        {
            Mapdata mapdata = _mapModel.MapDatas;
            return mapdata;
        }

    }
}
