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
            EventSystem.Register<AfterPlayerTurnBeginEvent>(v => OnPlayerTurnBegin(v));
        }

        private void OnPlayerTurnBegin(AfterPlayerTurnBeginEvent v)
        {
           HexCell[] cells= GetRoundHexCell(Player.instance.CurHexCell.Pos,2);
            foreach(var cell in cells)
            {
                cell.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        }

        private void OnMapInitFinish(MapInitFinishEvent @event)
        {
            Debug.Log($"地图加载完毕:{@event.Level}");
        }
    }
}
