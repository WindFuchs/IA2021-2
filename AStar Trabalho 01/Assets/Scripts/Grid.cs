using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPosition;
    public LayerMask WallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float Distance;
    Node[,] gridArray;
    public List<Node> FinalPath;
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        gridArray = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool Wall = true;

                if (Physics.CheckSphere(worldPoint, nodeRadius, WallMask))
                {
                    Wall = false;
                }
                gridArray[y, x] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 _worldPosition)
    {
        float xPoint = ((_worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float yPoint = ((_worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        xPoint = Mathf.Clamp01(xPoint);
        yPoint = Mathf.Clamp01(yPoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return gridArray[x, y];
    }

    private void NodeCheck(int _x_Check, int _y_Check, List<Node> _neighbourNodes)
    {
        if (_x_Check >= 0 && _x_Check < gridSizeX)
        {
            if (_y_Check >= 0 && _y_Check < gridSizeY)
            {
                _neighbourNodes.Add(gridArray[_x_Check, _y_Check]);
            }
        }
    }

    public List<Node> GetNeighbourNodes(Node _node)
    {
        List<Node> NeighbourNodes = new List<Node>();
        int x_Check;
        int y_Check;

        x_Check = _node.gridX + 1;
        y_Check = _node.gridY;

        // NodeCheck(x_Check, y_Check, NeighbourNodes);

        //Dreita
        if (x_Check >= 0 && x_Check < gridSizeX)
        {
            if (y_Check >= 0 && y_Check < gridSizeY)
            {
                NeighbourNodes.Add(gridArray[x_Check, y_Check]);
            }
        }

        //Esquerda
        x_Check = _node.gridX - 1;
        y_Check = _node.gridY;
        if (x_Check >= 0 && x_Check < gridSizeX)
        {
            if (y_Check >= 0 && y_Check < gridSizeY)
            {
                NeighbourNodes.Add(gridArray[x_Check, y_Check]);
            }
        }

        //Cima
        x_Check = _node.gridX;
        y_Check = _node.gridY + 1;
        if (x_Check >= 0 && x_Check < gridSizeX)
        {
            if (y_Check >= 0 && y_Check < gridSizeY)
            {
                NeighbourNodes.Add(gridArray[x_Check, y_Check]);
            }
        }

        //Baixo
        x_Check = _node.gridX;
        y_Check = _node.gridY - 1;
        if (x_Check >= 0 && x_Check < gridSizeX)
        {
            if (y_Check >= 0 && y_Check < gridSizeY)
            {
                NeighbourNodes.Add(gridArray[x_Check, y_Check]);
            }
        }

        return NeighbourNodes;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (gridArray != null)
        {
            foreach (Node n in gridArray)
            {
                if (n.Iswall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (FinalPath != null)
                {
                    if (FinalPath.Contains(n))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(n.Position, Vector3.one * (nodeDiameter - Distance));
            }
        }
    }

}
