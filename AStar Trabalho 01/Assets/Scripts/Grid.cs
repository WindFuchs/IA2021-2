using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform StartPosition;
    public LayerMask WallMask;
    public Vector2 vGridWorldSize;
    public float fNodeRadius;
    public float fDistanceBetweenNodes;

    Node[,] NodeArray;
    public List<Node> FinalPath;


    float fNodeDiameter;
    int iGridSizeX, iGridSizeY;


    private void Start()
    {
        fNodeDiameter = fNodeRadius * 2;//Double the radius to get diameter
        iGridSizeX = Mathf.RoundToInt(vGridWorldSize.x / fNodeDiameter);
        iGridSizeY = Mathf.RoundToInt(vGridWorldSize.y / fNodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        NodeArray = new Node[iGridSizeX, iGridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * vGridWorldSize.x / 2 - Vector3.forward * vGridWorldSize.y / 2;
        for (int x = 0; x < iGridSizeX; x++)
        {
            for (int y = 0; y < iGridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * fNodeDiameter + fNodeRadius) + Vector3.forward * (y * fNodeDiameter + fNodeRadius);
                bool Wall = true;




                if (Physics.CheckSphere(worldPoint, fNodeRadius, WallMask))
                {
                    Wall = false;
                }

                NodeArray[x, y] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    private void CheckNodes(int icheckX, int icheckY, List<Node> nodesList)
    {
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                nodesList.Add(NodeArray[icheckX, icheckY]);
            }
        }
    }

    public List<Node> GetNeighboringNodes(Node a_NeighborNode)
    {
        List<Node> NeighborList = new List<Node>();

        CheckNodes(a_NeighborNode.iGridX + 1, a_NeighborNode.iGridY, NeighborList);
        CheckNodes(a_NeighborNode.iGridX - 1, a_NeighborNode.iGridY, NeighborList);
        CheckNodes(a_NeighborNode.iGridX, a_NeighborNode.iGridY + 1, NeighborList);
        CheckNodes(a_NeighborNode.iGridX, a_NeighborNode.iGridY - 1, NeighborList);

        return NeighborList;
    }


    public Node NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + vGridWorldSize.x / 2) / vGridWorldSize.x);
        float iyPos = ((a_vWorldPos.z + vGridWorldSize.y / 2) / vGridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((iGridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((iGridSizeY - 1) * iyPos);

        return NodeArray[ix, iy];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(vGridWorldSize.x, 1, vGridWorldSize.y));
        if (NodeArray != null)
        {
            foreach (Node n in NodeArray)
            {
                if (n.bIsWall)
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
                Gizmos.DrawCube(n.vPosition, Vector3.one * (fNodeDiameter - fDistanceBetweenNodes));
            }
        }
    }
}
