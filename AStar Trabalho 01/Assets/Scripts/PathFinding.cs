using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Grid grid;
    public Transform StartingPosition;
    public Transform TargetPosition;
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void Update()
    {
        FindPath(StartingPosition.position, TargetPosition.position);
    }

    void FindPath(Vector3 _startingPosition, Vector3 _targetPosition)
    {
        Node StartNode = grid.NodeFromWorldPosition(_startingPosition);
        Node TargetNode = grid.NodeFromWorldPosition(_targetPosition);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);
        while (OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].CostF < CurrentNode.CostF || OpenList[i].CostF == CurrentNode.CostF && OpenList[i].CostH < CurrentNode.CostH)
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach (Node NeighbourNode in grid.GetNeighbourNodes(CurrentNode))
            {
                if (!NeighbourNode.Iswall || ClosedList.Contains(NeighbourNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.CostG + GetManhattenDistance(CurrentNode, NeighbourNode);

                if (MoveCost < NeighbourNode.CostG || !OpenList.Contains(NeighbourNode))
                {
                    NeighbourNode.CostH = GetManhattenDistance(NeighbourNode, TargetNode);
                    NeighbourNode.Parent = CurrentNode;
                }

                if (!OpenList.Contains(NeighbourNode))
                {
                    OpenList.Add(NeighbourNode);
                }
            }
        }
    }

    void GetFinalPath(Node _startNode, Node _targetNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = _targetNode;

        while (CurrentNode != _startNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }

        FinalPath.Reverse();
        grid.FinalPath = FinalPath;
    }

    int GetManhattenDistance(Node _currentNode, Node _neighbourNode)
    {
        int ix = Mathf.Abs(_currentNode.gridX - _neighbourNode.gridX);
        int iy = Mathf.Abs(_currentNode.gridY - _neighbourNode.gridY);

        return ix + iy;
    }
}
