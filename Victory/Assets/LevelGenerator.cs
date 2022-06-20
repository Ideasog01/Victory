using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [SerializeField]
    private Vector2 gridSize;

    [SerializeField]
    private int startPos = 0;

    [SerializeField]
    private Transform roomPrefab;

    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private NavMeshSurface levelNavMesh;

    [Header("Entity Probabilities")]

    [SerializeField]
    private float enemyChance;

    [SerializeField]
    private float resourceChance;

    [SerializeField]
    private Transform skeletonKnightEnemyPrefab;

    [SerializeField]
    private Transform resourcePrefab;

    [SerializeField]
    private Transform dungeonExitPrefab;

    private List<Cell> gridCells = new List<Cell>();

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateLevel()
    {
        int cellX = Random.Range(1, (int)(gridSize.x - 1));
        int cellY = Random.Range(1, (int)(gridSize.y - 1));

        Instantiate(dungeonExitPrefab, new Vector3(cellX * offset.x, 3, -cellY * offset.y), Quaternion.identity);

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var newRoom = Instantiate(roomPrefab, new Vector3(x * offset.x, 0, -y * offset.y), Quaternion.identity, this.transform).GetComponent<RoomController>();
                newRoom.UpdateRoom(gridCells[Mathf.FloorToInt(x + y * gridSize.x)].status);

                if(x != 0 && y != 0)
                {
                    float enemyRand = Random.Range(0, 100);
                    float resourceRand = Random.Range(0, 100);

                    if(x != cellX && y != cellY)
                    {
                        if (enemyChance > enemyRand)
                        {
                            Instantiate(skeletonKnightEnemyPrefab, newRoom.transform.position + Vector3.up, Quaternion.identity);
                        }

                        if (resourceChance > resourceRand)
                        {
                            Instantiate(resourcePrefab, newRoom.transform.position + Vector3.up, Quaternion.identity);
                        }
                    }
                }

                newRoom.name += " " + x + "-" + y; 
            }
        }

        levelNavMesh.BuildNavMesh();
    }

    private void GenerateMap()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                gridCells.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();
        int k = 0;

        while(k < 1000)
        {
            k++;

            gridCells[currentCell].visited = true;

            //Check Neighbours Cell
            List<int> neighbours = CheckNeighbours(currentCell);

            if(neighbours.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbours[Random.Range(0, neighbours.Count)];

                if(newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        gridCells[currentCell].status[2] = true;
                        currentCell = newCell;
                        gridCells[currentCell].status[3] = true;
                    }
                    else
                    {
                        gridCells[currentCell].status[1] = true;
                        currentCell = newCell;
                        gridCells[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        gridCells[currentCell].status[3] = true;
                        currentCell = newCell;
                        gridCells[currentCell].status[2] = true;
                    }
                    else
                    {
                        gridCells[currentCell].status[0] = true;
                        currentCell = newCell;
                        gridCells[currentCell].status[1] = true;
                    }
                }
            }
        }

        GenerateLevel();
    }

    private List<int> CheckNeighbours(int cell)
    {
        List<int> neighbours = new List<int>();

        if(cell - gridSize.x >= 0 && !gridCells[Mathf.FloorToInt(cell-gridSize.x)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell - gridSize.x));
        }

        if (cell + gridSize.x < gridCells.Count && !gridCells[Mathf.FloorToInt(cell + gridSize.x)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell + gridSize.x));
        }

        if ((cell + 1) % gridSize.x != 0 && !gridCells[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell + 1));
        }

        if (cell % gridSize.x != 0 && !gridCells[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbours;
    }
}
