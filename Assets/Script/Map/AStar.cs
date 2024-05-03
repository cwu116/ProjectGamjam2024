using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public float X_Real { get; set; }
    public float Y_Real { get; set; }
    public bool IsObstacle { get; set; }
    public float G { get; set; } // 从起点到当前点的实际代价
    public float H { get; set; } // 从当前点到终点的估计代价（启发式）
    public float F { get { return G + H; } } // G和H的总和
    public Node Parent { get; set; } // 父节点，用于回溯路径

    public Node(int x, int y, float x_Real, float y_Real, bool isObstacle)
    {
        X = x;
        Y = y;
        X_Real = x_Real;
        Y_Real = y_Real;
        IsObstacle = isObstacle;
        G = H = 0;
        Parent = null;
    }

    public Node(int x, int y, bool isObstacle)
    {
        X = x;
        Y = y;
        IsObstacle = isObstacle;
        G = H = 0;
        Parent = null;
    }
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class AStar
{
    private Node[,] grid;
    private List<Node> openList;
    private HashSet<Node> closedList;
    private int gridRows, gridCols;

    public AStar(int[,] map)
    {
        gridRows = map.GetLength(0);
        gridCols = map.GetLength(1);
        grid = new Node[gridRows, gridCols];
        openList = new List<Node>();
        closedList = new HashSet<Node>();

        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridCols; y++)
            {
                grid[x, y] = new Node(x, y, false);
            }
        }
    }

    public AStar(HexCell[,] map)
    {
        gridRows = map.GetLength(0);
        gridCols = map.GetLength(1);
        grid = new Node[gridRows, gridCols];
        openList = new List<Node>();
        closedList = new HashSet<Node>();

        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridCols; y++)
            {
                HexCell hexCell = map[x, y];
                Vector3 hexCellPosition = hexCell.transform.position;
                //TODO::这里应当改成该网格上的物体是否是阻碍物
                grid[x, y] = new Node(x, y, hexCellPosition.x, hexCellPosition.y,false);
            }
        }
    }

    /// <summary>
    /// 这个A*构造函数是专门用来计算两点距离，而不考虑障碍物的
    /// </summary>
    /// <param name="map"></param>
    /// <param name="isGetDistance"></param>
    public AStar(HexCell[,] map, bool isGetDistance)
    {
        gridRows = map.GetLength(0);
        gridCols = map.GetLength(1);
        grid = new Node[gridRows, gridCols];
        openList = new List<Node>();
        closedList = new HashSet<Node>();

        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridCols; y++)
            {
                HexCell hexCell = map[x, y];
                Vector3 hexCellPosition = hexCell.transform.position;
                //TODO::这里应当改成该网格上的物体是否是阻碍物
                if(isGetDistance == true)
                {
                    grid[x, y] = new Node(x, y, hexCellPosition.x, hexCellPosition.y, false);
                }
                else
                {
                    //TODO::这里后续应当改成正常计算是否是障碍物
                    grid[x, y] = new Node(x, y, hexCellPosition.x, hexCellPosition.y, false);
                }
            }
        }
    }

    public List<Point> FindPath(Point start, Point end)
    {
        Node startNode = grid[start.X, start.Y];
        Node endNode = grid[end.X, end.Y];
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < currentNode.F || openList[i].F == currentNode.F && openList[i].H < currentNode.H)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (neighbor.IsObstacle || closedList.Contains(neighbor)) continue;

                float newMovementCostToNeighbor = currentNode.G + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.G || !openList.Contains(neighbor))
                {
                    neighbor.G = newMovementCostToNeighbor;
                    neighbor.H = GetDistance(neighbor, endNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        return new List<Point>(); // 返回空路径
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        int[,] hexOffsets = {
        { 1, 0 }, { 0, 1 }, { -1, 1 },
        { -1, 0 }, { -1, -1 }, { 0, -1 }
        };

        int[,] hexoffsets2 =
        {
            {1, 0 }, {1,1},{0,1},
            {-1,0 }, {0,-1},{1,-1}
        };
        if(node.Y % 2 == 0)
        {
            // 遍历相邻的六边形节点
            for (int i = 0; i < 6; i++)
            {
                int xOffset = hexOffsets[i, 0];
                int yOffset = hexOffsets[i, 1];

                int checkX = node.X + xOffset;
                int checkY = node.Y + yOffset;

                if (checkX >= 0 && checkX < gridRows && checkY >= 0 && checkY < gridCols)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        else
        {
            // 遍历相邻的六边形节点
            for (int i = 0; i < 6; i++)
            {
                int xOffset = hexoffsets2[i, 0];
                int yOffset = hexoffsets2[i, 1];

                int checkX = node.X + xOffset;
                int checkY = node.Y + yOffset;

                if (checkX >= 0 && checkX < gridRows && checkY >= 0 && checkY < gridCols)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        

        return neighbors;

    }

    private List<Point> RetracePath(Node startNode, Node endNode)
    {
        List<Point> path = new List<Point>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(new Point(currentNode.X, currentNode.Y));
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }

    private float GetDistance(Node nodeA, Node nodeB)
    {
        float dstX = Math.Abs(nodeA.X_Real - nodeB.X_Real);
        float dstY = Math.Abs(nodeA.Y_Real - nodeB.Y_Real);
        float res = 0;
        res = dstX * dstX + dstY * dstY;
        return res;
    }
}
