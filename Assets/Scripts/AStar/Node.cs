using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    
    //Class for a given node on the grid
    
    //Whether or not this node can be walked on
    public bool walkable;
    //Nodes position
    public Vector2 worldPosition;

    //position on the grid
    public int gridX;
    public int gridY;

    //Distance from this node to the starting position
    public int gCost;
    //distance from this node to the target position
    public int hCost;

    public Node parent;

    private int heapIndex;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost => gCost + hCost;

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
}
