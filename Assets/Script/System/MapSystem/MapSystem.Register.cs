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
            EventSystem.Register<AfterPlayerTurnBeginEvent>(v => HighLightCells(Player.instance.MoveTimes>0?1:0));
            EventSystem.Register<PlayerMoveEvent>(v => { ClearHighlightCells(); HighLightCells(v.moveTimes > 0 ? 1 : 0); });
            EventSystem.Register<AfterPlayerTurnEndEvent>(v => ClearHighlightCells());
        }


        private void OnMapInitFinish(MapInitFinishEvent @event)
        {
            Debug.Log($"地图加载完毕:{@event.Level}");
        }

        private void HighLightCells(int distance=1)
        {
            HexCell[] cells = GetRoundHexCell(Player.instance.CurHexCell.Pos, distance);
            foreach (var cell in cells)
            {
                cell.GetComponentInChildren<SpriteRenderer>().color = Color.red;//测试，需修改
                cell.IsHightlight = true;
            }
        }

        private void ClearHighlightCells()
        {
           foreach(var cell in GridManager.Instance.hexCells)
            {
                if(cell.IsHightlight)
                {
                    cell.GetComponentInChildren<SpriteRenderer>().color = Color.white;//测试，需修改
                    cell.IsHightlight = false;
                }
            }
        }
    }
}
