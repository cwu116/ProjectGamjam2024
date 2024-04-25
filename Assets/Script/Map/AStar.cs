using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsObstacle { get; set; }
    public float G { get; set; } // 从起点到当前点的实际代价
    public float H { get; set; } // 从当前点到终点的估计代价（启发式）
    public float F { get { return G + H; } } // G和H的总和
    public Node Parent { get; set; } // 父节点，用于回溯路径

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
                grid[x, y] = new Node(x, y, map[x, y] == 1);
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
                //TODO::这里应当改成该网格上的物体是否是阻碍物
                grid[x, y] = new Node(x, y, false);
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

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; // 跳过自身

                int checkX = node.X + x;
                int checkY = node.Y + y;

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
        int dstX = Math.Abs(nodeA.X - nodeB.X);
        int dstY = Math.Abs(nodeA.Y - nodeB.Y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
