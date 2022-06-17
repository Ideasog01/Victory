using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static List<Node> nodeList = new List<Node>();

    [SerializeField]
    private int gridWidth;

    [SerializeField]
    private int gridHeight;

    [SerializeField]
    private int nodeDimension;

    private void Awake()
    {
        GenerateGrid();
    }

    public Node FindNode(Vector3 position)
    {
        foreach(Node node in nodeList)
        {
            float distance = Vector3.Distance(position, node.NodePosition);

            if(distance < nodeDimension)
            {
                return node;
            }
        }

        Debug.LogWarning("NODE RETURNED WAS NULL");
        return null;
    }

    private void GenerateGrid()
    {
        for(int x = -(gridWidth / 2); x < gridWidth; x++)
        {
            for(int y = -(gridHeight / 2); y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x * nodeDimension, 0, y * nodeDimension);
                Node node = new Node();
                node.NodePosition = position;
                nodeList.Add(node);
            }
        }
    }
}
