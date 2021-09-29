using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;
    public bool Iswall;
    public Vector3 Position;
    public Node Parent;
    public int CostG;
    public int CostH;
    public int CostF { get { return CostG + CostH; } }
    public Node(bool _isWall, Vector3 _position, int _gridX, int _gridY)
    {
        Iswall = _isWall;
        Position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }
}
