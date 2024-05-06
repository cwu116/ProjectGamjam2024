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
            EventSystem.Register<AfterPlayerTurnBeginEvent>(v => HighLightCells(Player.instance.MoveTimes>0?Player.instance.StepLength>0?1:0:0));
            EventSystem.Register<PlayerMoveEvent>(v => { ClearHighlightCells(); HighLightCells(Player.instance.MoveTimes > 0 ? Player.instance.StepLength > 0 ? 1 : 0 : 0); });
            EventSystem.Register<UsePotionEvent>(v=> { ClearHighlightCells(); HighLightCells(Player.instance.MoveTimes > 0 ? Player.instance.StepLength > 0 ? 1 : 0 : 0); });
            EventSystem.Register<AfterPlayerTurnEndEvent>(v => ClearHighlightCells());

            EventSystem.Register<HighLightAttackBlockEvent>(v => { ClearEnemyAttackCells(); HighlightEnemyAttackCells(v.pos, v.distance); });
            EventSystem.Register<ClearAttackBlockEvent>(v => ClearEnemyAttackCells());
            EventSystem.Register<HighLightWarningBlockEvent>(v => { ClearEnemyWarningCells(); HighlightEnemyWarningCells(v.pos, v.distance); });
            EventSystem.Register<ClearWarningBlockEvent>(v => ClearEnemyWarningCells());
        }


        private void OnMapInitFinish(MapInitFinishEvent @event)
        {
            Debug.Log($"地图加载完毕:{@event.Level}");
        }

        //Dictionary<HexCell, Sprite> tempSprites = new Dictionary<HexCell, Sprite>();
        List<HexCell> tempCells = new List<HexCell>();
        public void HighLightCells(int distance=1)
        {
            tempCells.Clear();
            HexCell[] cells = GetRoundHexCell(Player.instance.CurHexCell.Pos, distance);
            foreach (var cell in cells)
            {
                tempCells.Add(cell);
                cell.transform.Find("highLightBlock").gameObject.SetActive(true);
                cell.IsHightlight = true;
            }
        }

        public void ClearHighlightCells()
        {
           foreach(var cell in tempCells)
            {
                cell.transform.Find("highLightBlock").gameObject.SetActive(false);
                cell.IsHightlight = false;
            }
        }






        List<HexCell> enemyWarningtempCells = new List<HexCell>();
        private void HighlightEnemyWarningCells(Vector2 pos,int distance)
        {
            enemyWarningtempCells.Clear();
            HexCell[] cells = GetRoundHexCell(pos, distance);
            foreach (var cell in cells)
            {
                enemyWarningtempCells.Add(cell);
                cell.transform.Find("enemyWarningBlock").gameObject.SetActive(true);
                cell.IsHightlight = true;
            }
        }

        private void ClearEnemyWarningCells()
        {
            foreach (var cell in enemyWarningtempCells)
            {
                cell.transform.Find("enemyWarningBlock").gameObject.SetActive(false);
                Debug.Log(cell.transform.Find("enemyWarningBlock").gameObject.activeInHierarchy);
                cell.IsHightlight = false;
            }
        }





        List<HexCell> enemyAttacktempCells = new List<HexCell>();
        private void HighlightEnemyAttackCells(Vector2 pos, int distance)
        {
            enemyAttacktempCells.Clear();
            HexCell[] cells = GetRoundHexCell(pos, distance);
            foreach (var cell in cells)
            {
                enemyAttacktempCells.Add(cell);
                cell.transform.Find("enemyAttackBlock").gameObject.SetActive(true);
                cell.IsHightlight = true;
            }
        }

        private void ClearEnemyAttackCells()
        {
            foreach (var cell in enemyAttacktempCells)
            {
                cell.transform.Find("enemyAttackBlock").gameObject.SetActive(false);
                cell.IsHightlight = false;
            }
        }
    }
}

