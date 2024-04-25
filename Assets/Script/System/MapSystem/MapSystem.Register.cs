using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Model;
using Game.System;
using UnityEngine;

namespace Game.System
{
    public partial class MapSystem : BaseSystem
    {
        private void RegisterEvent()
        {
            EventSystem.Register<MapInitFinishEvent>(OnMapInitFinish);
        }

        private void OnMapInitFinish(MapInitFinishEvent @event)
        {
            Debug.Log($"地图加载完毕:{@event.Level}");
        }
    }
}
