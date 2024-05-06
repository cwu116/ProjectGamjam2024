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
    interface MapFunction
    {
        int CalculateDistance(Vector2 origin, Vector2 target);//计算两点间距离
        HexCell[] GetRoundHexCell(Vector2 position, int radius);//获得某个位置周围的格子
        Vector2 RandomPatrol(Vector2 originPosition, Vector2 nowPosition);//敌人在出生点附近随机移动

        List<HexCell> GetPath(Vector2 origin, Vector2 target);//获得完整寻路路径

        Vector2 GetNextPosWorld(Vector2 origin, Vector2 target); //获取当玩家进入警戒范围时，移动到的下一格目标世界位置
        List<HexCell> StraightRunPath(Vector2 origin, Vector2 target); //获取直线冲刺的路径
    }
    public partial class MapSystem : BaseSystem,MapFunction
    {
        private int height;
        private int width;

        // 定义偶数六方向偏移量
        private int[][] evenRowOffsets = new int[][]
        {
                new int[] { 1, 0 },   
                new int[] { 0, 1 },  
                new int[] { -1, 1 }, 
                new int[] { -1, 0 },  
                new int[] { -1, -1 },  
                new int[] { 0, -1 }    
        };

        //定义奇数六方向偏移量
        private int[][] oddRowOffsets = new int[][]
        {
                new int[] { 1, 0 },   
                new int[] { 1, 1 },  
                new int[] { 0, 1 },  
                new int[] { -1, 0 },  
                new int[] { 0, -1 },   
                new int[] { 1, -1 }    
        };
        public void InitHW()
        {
            height = GridManager.Instance.hexCells.GetLength(0);
            width = GridManager.Instance.hexCells.GetLength(1);
        }

        /// <summary>
        /// 获取当玩家进入警戒范围时，移动到的下一格目标世界位置
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Vector2 GetNextPosWorld(Vector2 origin, Vector2 target)
        {
            HexCell[,] hexCells = GridManager.Instance.hexCells;
            AStar aStar = new AStar(hexCells);
            Point start = new Point((int)origin.x, (int)origin.y);
            Point end = new Point((int)target.x, (int)target.y);
            List<Point> path = aStar.FindPath(start, end);
            if(path != null)
            {
                Vector2 res = new Vector2(path[0].X, path[0].Y);
                return res;
            }
            return Vector2.zero;
        }

        /// <summary>
        /// 得到完整格子路径
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<HexCell> GetPath(Vector2 origin, Vector2 target)
        {
            HexCell[,] hexCells = GridManager.Instance.hexCells;
            AStar aStar = new AStar(hexCells);
            Point start = new Point((int)origin.x, (int)origin.y);
            Point end = new Point((int)target.x, (int)target.y);
            List<Point> path = aStar.FindPath(start, end);
            List<HexCell> pathHexCell = new List<HexCell>();
            if (path != null)
            {
                for(int i = 0; i < path.Count; i++)
                {
                    pathHexCell.Add(hexCells[path[i].X, path[i].Y]);
                }
                return pathHexCell;
            }
            return null;
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

        /// <summary>
        /// 计算两点之间的距离
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int CalculateDistance(Vector2 origin, Vector2 target)
        {
            HexCell[,] hexCells = GridManager.Instance.hexCells;
            AStar aStar = new AStar(hexCells,true);
            Point start = new Point((int)origin.x, (int)origin.y);
            Point end = new Point((int)target.x, (int)target.y);
            List<Point> path = aStar.FindPath(start, end);
            if(path != null)
            {
                return path.Count;
            }
            return 0;
        }

        /// <summary>
        /// 返回某个位置周围的格子
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public HexCell[] GetRoundHexCell(Vector2 position, int radius)
        {
            int x = (int)position.x;
            int y = (int)position.y;
            List<Vector2> roundIndex = GetNeighborIndices(x, y, radius);
            HexCell[,] hexCells = GridManager.Instance.hexCells;
            HexCell[] roundHexCells = new HexCell[roundIndex.Count];
            for(int i = 0; i < roundIndex.Count; i++)
            {
                int a = (int)roundIndex[i][0];
                int b = (int)roundIndex[i][1];
                roundHexCells[i] = hexCells[a, b];
            }
            if(roundHexCells != null)
            {
                return roundHexCells;
            }
            return null;
        }

        /// <summary>
        /// 递归获取邻居网格
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private List<Vector2> GetNeighborIndices(int x, int y, int n)
        {
            List<Vector2> neighborIndices = new List<Vector2>();

            int[][] offsets = (y % 2 == 0) ? evenRowOffsets : oddRowOffsets;

            // 递归找到相邻格子的索引
            FindNeighborIndices(x, y, n, offsets, neighborIndices);

            return neighborIndices;
        }

        private void FindNeighborIndices(int x, int y, int n, int[][] offsets, List<Vector2> neighborIndices)
        {
            if (n <= 0) return;

            // 根据偏移量计算相邻格子的索引
            for (int i = 0; i < 6; i++)
            {
                int nx = x + offsets[i][0];
                int ny = y + offsets[i][1];

                // 检查相邻格子的索引是否在范围内
                if (nx >= 0 && nx < height && ny >= 0 && ny < width)
                {
                    Vector2 index = new Vector2(nx, ny);
                    if (!neighborIndices.Contains(index))
                    {
                        neighborIndices.Add(index);
                    }
                    Debug.Log(index[0] + " " + index[1]);
                    int[][] newoffsets = (ny % 2 == 0) ? evenRowOffsets : oddRowOffsets;
                    // 递归或循环找到下一个相邻格子的索引
                    FindNeighborIndices(nx, ny, n - 1, newoffsets, neighborIndices);
                }
            }
        }

        /// <summary>
        /// 检查列表中是否包含指定索引
        /// </summary>
        /// <param name="indices"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool ContainsIndex(List<int[]> indices, int x, int y)
        {
            foreach (int[] index in indices)
            {
                if (index[0] == x && index[1] == y)
                {
                    return true;
                }
            }
            return false;
        }

        public Vector2 RandomPatrol(Vector2 originPosition, Vector2 nowPosition)
        {
            int x = (int)originPosition.x;
            int y = (int)originPosition.y;
            List<Vector2> originLis = GetNeighborIndices(x, y, 1);
            originLis.Add(new Vector2(x,y));
            List<Vector2> nowLis = GetNeighborIndices((int)nowPosition.x, (int)nowPosition.y, 1);
            bool isNotFound = true;
            int num = 0;
            while (isNotFound)
            {
                int n = UnityEngine.Random.Range(0, originLis.Count - 1);
                int a = (int)originLis[n][0];
                int b = (int)originLis[n][1];
                for(int i = 0; i < nowLis.Count; i++)
                {
                    if(nowLis[i][0] == a && nowLis[i][1] == b)
                    {
                        isNotFound = false;
                        return new Vector2(a, b);
                    }
                }
                num++;
                if(num > 20)
                {
                    isNotFound = false;
                }
            }

            return Vector2.zero;
        }

        private Vector2[] dirs = new Vector2[] { new Vector2(0.87f, 0.5f), new Vector2(0f, 1f), new Vector2(-0.87f, 0.5f), new Vector2(-0.87f, -0.5f), new Vector2(0f, -1f), new Vector2(0.87f, -0.5f) };

        /// <summary>
        /// 冲锋怪直线冲刺
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<HexCell> StraightRunPath(Vector2 origin, Vector2 target)
        {
            Vector2 direction = target - origin;
            Vector2 runDirection = Vector2.zero;
            foreach(Vector2 dir in dirs)
            {
                if(CalculateVector2Angle(direction,dir) <= 30)
                {
                    runDirection = dir;
                    break;
                }
            }
            List<HexCell> straightPath = new List<HexCell>();
            if(runDirection == Vector2.zero)
            {
                return null;
            }
            CreateStraightPath(runDirection, origin, straightPath);
            List<HexCell> res = new List<HexCell>();
            foreach(var cell in straightPath)
            {
                if(cell.OccupyObject != null && cell.OccupyObject.GetComponent<BaseEntity>().IsObstacle == true)
                {
                    break;
                }
                else
                {
                    res.Add(cell);
                }
            }
            if(res != null)
            {
                return res;
            }
            return null;
        }
        private float CalculateVector2Angle(Vector2 a, Vector2 b)
        {
            return Vector2.Angle(a, b);
        }
        private void CreateStraightPath(Vector2 runDirection, Vector2 origin, List<HexCell> straightPath)
        {
            if (runDirection == Vector2.zero)
            {
                return;
            }
            else if (runDirection == dirs[0])
            {
                if (origin.y % 2 == 0)
                {
                    if ((int)origin.x < height && (int)origin.y + 1 < width && (int)origin.x > 0 && (int)origin.y + 1 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x, (int)origin.y + 1]);
                    }
                    if ((int)origin.x + 1 < height && (int)origin.y + 2 < width && (int)origin.x + 1 > 0 && (int)origin.y + 2 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y + 2]);
                    }
                    if ((int)origin.x + 1 < height && (int)origin.y + 3 < width && (int)origin.x + 1 > 0 && (int)origin.y + 3 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y + 3]);
                    }
                }
                else
                {
                    if ((int)origin.x + 1 < height && (int)origin.y + 1 < width && (int)origin.x + 1 > 0 && (int)origin.y + 1 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y + 1]);
                    }
                    if ((int)origin.x + 1 < height && (int)origin.y + 2 < width && (int)origin.x + 1 > 0 && (int)origin.y + 2 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y + 2]);
                    }
                    if ((int)origin.x + 2 < height && (int)origin.y + 3 < width && (int)origin.x + 2 > 0 && (int)origin.y + 3 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 2, (int)origin.y + 3]);
                    }
                }
            }
            else if (runDirection == dirs[1])
            {
                if ((int)origin.x + 1 < height && (int)origin.y < width && (int)origin.x + 1 > 0 && (int)origin.y > 0)
                {
                    straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y]);
                }
                if ((int)origin.x + 2 < height && (int)origin.y < width && (int)origin.x + 2 > 0 && (int)origin.y > 0)
                {
                    straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 2, (int)origin.y]);
                }
                if ((int)origin.x + 3 < height && (int)origin.y < width && (int)origin.x + 3 > 0 && (int)origin.y > 0)
                {
                    straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 3, (int)origin.y]);
                }
            }
            else if (runDirection == dirs[2])
            {
                if (origin.y % 2 == 0)
                {
                    if ((int)origin.x < height && (int)origin.y - 1 < width && (int)origin.x > 0 && (int)origin.y - 1 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x, (int)origin.y - 1]);
                    }
                    if ((int)origin.x + 1 < height && (int)origin.y - 2 < width && (int)origin.x + 1 > 0 && (int)origin.y - 2 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y - 2]);
                    }
                    if ((int)origin.x + 1 < height && (int)origin.y - 3 < width && (int)origin.x + 1 > 0 && (int)origin.y - 3 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y - 3]);
                    }

                }
                else
                {
                    if ((int)origin.x + 1 < height && (int)origin.y - 1 < width && (int)origin.x + 1 > 0 && (int)origin.y - 1 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y - 1]);
                    }
                    if ((int)origin.x + 1 < height && (int)origin.y - 2 < width && (int)origin.x + 1 > 0 && (int)origin.y - 2 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 1, (int)origin.y - 2]);
                    }
                    if ((int)origin.x + 2 < height && (int)origin.y - 3 < width && (int)origin.x + 2 > 0 && (int)origin.y - 3 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x + 2, (int)origin.y - 3]);
                    }
                }
            }
            else if (runDirection == dirs[3])
            {
                if (origin.y % 2 == 0)
                {
                    if ((int)origin.x - 1 < height && (int)origin.y - 1 < width && (int)origin.x - 1 > 0 && (int)origin.y - 1 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y - 1]);
                    }
                    if ((int)origin.x - 1 < height && (int)origin.y - 2 < width && (int)origin.x - 1 > 0 && (int)origin.y - 2 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y - 2]);
                    }
                    if ((int)origin.x - 2 < height && (int)origin.y - 3 < width && (int)origin.x - 2 > 0 && (int)origin.y - 3 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 2, (int)origin.y - 3]);
                    }
                }
                else
                {
                    if ((int)origin.x < height && (int)origin.y - 1 < width && (int)origin.x > 0 && (int)origin.y - 1 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x, (int)origin.y - 1]);
                    }
                    if ((int)origin.x - 1 < height && (int)origin.y - 2 < width && (int)origin.x - 1 > 0 && (int)origin.y - 2 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y - 2]);
                    }
                    if ((int)origin.x - 1 < height && (int)origin.y - 3 < width && (int)origin.x - 1 > 0 && (int)origin.y - 3 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y - 3]);
                    }
                }
            }
            else if (runDirection == dirs[4])
            {
                if ((int)origin.x - 1 < height && (int)origin.y < width && (int)origin.x - 1 > 0 && (int)origin.y > 0)
                {
                    straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y]);
                }
                if ((int)origin.x - 2 < height && (int)origin.y < width && (int)origin.x - 2 > 0 && (int)origin.y > 0)
                {
                    straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 2, (int)origin.y]);
                }
                if ((int)origin.x - 3 < height && (int)origin.y < width && (int)origin.x - 3 > 0 && (int)origin.y > 0)
                {
                    straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 3, (int)origin.y]);
                }
            }
            else if (runDirection == dirs[5])
            {
                if (origin.y % 2 == 0)
                {
                    if ((int)origin.x - 1 < height && (int)origin.y + 1 < width && (int)origin.x - 1 > 0 && (int)origin.y + 1 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y + 1]);
                    }
                    if ((int)origin.x - 1 < height && (int)origin.y + 2 < width && (int)origin.x - 1 > 0 && (int)origin.y + 2 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y + 2]);
                    }
                    if ((int)origin.x - 2 < height && (int)origin.y + 3 < width && (int)origin.x - 2 > 0 && (int)origin.y + 3 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 2, (int)origin.y + 3]);
                    }
                }
                else
                {
                    if ((int)origin.x < height && (int)origin.y + 1 < width && (int)origin.x > 0 && (int)origin.y + 1 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x, (int)origin.y + 1]);
                    }
                    if ((int)origin.x - 1 < height && (int)origin.y + 2 < width && (int)origin.x - 1 > 0 && (int)origin.y + 2 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y + 2]);
                    }
                    if ((int)origin.x - 1 < height && (int)origin.y + 3 < width && (int)origin.x - 1 > 0 && (int)origin.y + 3 > 0)
                    {
                        straightPath.Add(GridManager.Instance.hexCells[(int)origin.x - 1, (int)origin.y + 3]);
                    }
                }
            }
        }

    }
}
