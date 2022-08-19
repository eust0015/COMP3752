using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
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

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost => gCost + hCost;
}
