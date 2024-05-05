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

        Dictionary<HexCell, Sprite> tempSprites = new Dictionary<HexCell, Sprite>();
        private void HighLightCells(int distance=1)
        {
            tempSprites.Clear();
            HexCell[] cells = GetRoundHexCell(Player.instance.CurHexCell.Pos, distance);
            foreach (var cell in cells)
            {
                tempSprites[cell] = cell.GetComponentInChildren<SpriteRenderer>().sprite;
                cell.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "地块-选中");
                cell.IsHightlight = true;
            }
        }

        private void ClearHighlightCells()
        {
           foreach(var element in tempSprites)
            {
                element.Key.GetComponentInChildren<SpriteRenderer>().sprite = element.Value;
            }
        }
    }
}
