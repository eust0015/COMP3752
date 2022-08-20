using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] private bool showGrid = false;

    public Transform player;
    public LayerMask unwalkableMask;
    
    //Size of the grid
    public Vector2 gridWorldSize;
    //Physical size of each node
    public float nodeRadius;
    //2d array of the nodes
    private Node[,] grid;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    public int maxSize => gridSizeX * gridSizeY;
    
    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        //Creates a grid of nodes from the bottom left of the defined size of the world up.
        grid = new Node[gridSizeX, gridSizeY];

        Vector2 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) +
                                     Vector2.up * (y * nodeDiameter + nodeRadius);
                //Checks if anything on the given position is unwalkable, if so, make the node not walkable.
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask) != null);
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x==0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX,checkY]);
                }
            }
        }

        return neighbours;
    }
    
    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        //Call to get the node linked to a given world position
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }
    
    void OnDrawGizmos()
    {
        //gidmo
        Gizmos.DrawWireCube(transform.position, gridWorldSize);

        if (grid != null)
        {
            if (showGrid)
            {
                Node playerNode = NodeFromWorldPoint(player.position);
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? new Color(0,1,1,0.3f): new Color(1,0,0,0.3f);
                    if (playerNode == n)
                    {
                        Gizmos.color = new Color(0, 1, 0, 0.3f);
                    }
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
                {
                
                }
            }
        }
    }
}
