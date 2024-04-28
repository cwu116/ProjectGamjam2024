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
        private int height;
        private int width;
        private void InitHW()
        {
            height = GridManager.Instance.hexCells.GetLength(0);
            width = GridManager.Instance.hexCells.GetLength(1);
        }

        /// <summary>
        /// ��ȡ����ҽ��뾯�䷶Χʱ���ƶ�������һ��Ŀ������λ��
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
        /// ��ȡ����ҽ�����׷�Χʱ���ƶ�������һ��Ŀ���ͼλ��
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
        /// ��������֮��ľ���
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
        /// ����ĳ��λ����Χ�ĸ���
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public HexCell[] GetRoundHexCell(Vector2 position, int radius)
        {
            int x = (int)position.x;
            int y = (int)position.y;
            List<int[]> roundIndex = GetNeighborIndices(x, y, radius);
            HexCell[,] hexCells = GridManager.Instance.hexCells;
            HexCell[] roundHexCells = new HexCell[roundIndex.Count];
            for(int i = 0; i < roundIndex.Count; i++)
            {
                int a = roundIndex[i][0];
                int b = roundIndex[i][1];
                roundHexCells[i] = hexCells[a, b];
            }
            if(roundHexCells != null)
            {
                return roundHexCells;
            }
            return null;
        }

        /// <summary>
        /// �ݹ��ȡ�ھ�����
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private List<int[]> GetNeighborIndices(int x, int y, int n)
        {
            List<int[]> neighborIndices = new List<int[]>();

            // �������������ƫ����
            int[][] evenRowOffsets = new int[][]
            {
                new int[] { 1, 0 },   // ��
                new int[] { 0, -1 },  // ����
                new int[] { -1, -1 }, // ����
                new int[] { -1, 0 },  // ��
                new int[] { -1, 1 },  // ����
                new int[] { 0, 1 }    // ����
            };

            int[][] oddRowOffsets = new int[][]
            {
                new int[] { 1, 0 },   // ��
                new int[] { 1, -1 },  // ����
                new int[] { 0, -1 },  // ����
                new int[] { -1, 0 },  // ��
                new int[] { 0, 1 },   // ����
                new int[] { 1, 1 }    // ����
            };

            int[][] offsets = (y % 2 == 0) ? evenRowOffsets : oddRowOffsets;

            // �ݹ��ѭ���ҵ����ڸ��ӵ�����
            FindNeighborIndices(x, y, n, offsets, neighborIndices);

            return neighborIndices;
        }

        private void FindNeighborIndices(int x, int y, int n, int[][] offsets, List<int[]> neighborIndices)
        {
            if (n <= 0) return;

            // ����ƫ�����������ڸ��ӵ�����
            for (int i = 0; i < 6; i++)
            {
                int nx = x + offsets[i][0];
                int ny = y + offsets[i][1];

                // ������ڸ��ӵ������Ƿ��ڷ�Χ��
                if (nx >= 0 && nx < height && ny >= 0 && ny < width)
                {
                    int[] index = { nx, ny };
                    neighborIndices.Add(index);

                    // �ݹ��ѭ���ҵ���һ�����ڸ��ӵ�����
                    FindNeighborIndices(nx, ny, n - 1, offsets, neighborIndices);
                }
            }
        }

        public Vector2 RandomPatrol(Vector2 originPosition, Vector2 nowPosition)
        {
            int x = (int)originPosition.x;
            int y = (int)originPosition.y;
            List<int[]> originLis = GetNeighborIndices(x, y, 1);
            originLis.Add(new int[] { x, y });
            List<int[]> nowLis = GetNeighborIndices((int)nowPosition.x, (int)nowPosition.y, 1);
            bool isNotFound = true;
            int num = 0;
            while (isNotFound)
            {
                int n = UnityEngine.Random.Range(0, originLis.Count - 1);
                int a = originLis[n][0];
                int b = originLis[n][1];
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


    }
}
