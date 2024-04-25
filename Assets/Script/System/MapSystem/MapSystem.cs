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

        /// <summary>
        /// 获取当玩家进入警戒范围时，移动到的下一格目标世界位置
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private Vector2 GetNextPosWorld(Vector2 origin, Vector2 target)
        {
            HexCell[,] hexCells = GridManager.Instance.hexCells;
            AStar aStar = new AStar(hexCells);
            Point start = new Point((int)origin.x, (int)origin.y);
            Point end = new Point((int)target.x, (int)target.y);
            List<Point> path = aStar.FindPath(start, end);
            if(path != null)
            {
                Vector2 res = new Vector2(path[1].X, path[1].Y);
                return res;
            }
            return Vector2.zero;
        }

        /// <summary>
        /// 获取当玩家进入进阶范围时，移动到的下一格目标地图位置
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private int[] GetNextPosMap(int[] origin, int[] target)
        {
            HexCell[,] hexCells = GridManager.Instance.hexCells;
            AStar aStar = new AStar(hexCells);
            Point start = new Point(origin[0], origin[1]);
            Point end = new Point(target[0], target[1]);
            List<Point> path = aStar.FindPath(start, end);
            if (path != null)
            {
                int[] res = new int[2] {path[1].X, path[1].Y };
                return res;
            }
            return new int[] { };
        }

        private bool InspectTarget(Vector2 myPos, int radius)
        {
            HexCell[,] hexCells = GridManager.Instance.hexCells;
            List<int> ranges = new List<int>();
            for(int i = 1; i <= radius; i++)
            {
                ranges.Add(i * 6);
            }
            //TODO::继续查询每一圈的hexcells

            return false;
        }

        



        
    }
}
