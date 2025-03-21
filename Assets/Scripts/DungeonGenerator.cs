using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public NavMeshBaker navMeshBaker;

    public GameObject zombie;

    [System.Serializable]
    public class Rule
    {
        public int maxSpawnCount = int.MaxValue;

        [System.NonSerialized]
        public int currentSpawnCount = 0;
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligatory;
        public bool trigger;

        public bool CanSpawnMore()
        {
            return maxSpawnCount == -1 || currentSpawnCount < maxSpawnCount;
        }
        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn 1 - can spawn 2 - HAS to spawn

            if(trigger && x>= minPosition.x && x<=maxPosition.x && y >= minPosition.y && y <= maxPosition.y && x==y)
            {
                return 2;
            }
            
            if (x>= minPosition.x && x<=maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }

    }

    public Vector2Int size;
    public int startPos = 0;
    public Rule[] rooms;
    // public GameObject room;
    // public GameObject[] rooms;
    public Vector2 offset;

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    void GenerateDungeon()
    {
        // NavMeshSurface[] navMeshSurfaces;
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {
                    // int randomRoom = Random.Range(0, rooms.Length);
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        if(!rooms[k].CanSpawnMore()){
                            continue;
                        }

                        int p = rooms[k].ProbabilityOfSpawning(i, j);

                        if(p == 2)
                        {
                            randomRoom = k;
                            break;
                        } else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if(randomRoom == -1)
                    {
                        if (availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[Random.Range(1, availableRooms.Count-1)];
                        }
                        else
                        {
                            randomRoom = 3;
                        }
                    }

                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    rooms[randomRoom].currentSpawnCount++;
                    // var newRoom = Instantiate(rooms[randomRoom], new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    bool lightOn = i==0 && j==0 || i==size.x-1&&j==size.y-1 || Random.value > 0.5;
                    bool scaryVoice = Random.Range(0, 10) < 5;
                    newRoom.UpdateRoom(board[(i+j*size.x)].status, lightOn, zombie, scaryVoice);
                    newRoom.name += " " + i + "-" + j;

                    // navMeshSurfaces = newRoom.GetComponentsInChildren<NavMeshSurface>();
                    
                    // GameObject[] childrenArray = newRoom.GetComponentsInChildren<Transform>().Where(t => t.name == "Floor").Select(t => t.gameObject).ToArray();
                }
            }
        }
        NavMeshSurface[] surfaces = GetComponentsInChildren<NavMeshSurface>();
        navMeshBaker.BakeNavMesh(surfaces);
    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k<1000)
        {
            k++;

            board[currentCell].visited = true;

            if(currentCell == board.Count - 1)
            {
                break;
            }

            //Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
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

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }

            }

        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighbor
        if (cell - size.x >= 0 && !board[(cell-size.x)].visited)
        {
            neighbors.Add((cell - size.x));
        }

        //check down neighbor
        if ((cell + size.x < board.Count) && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        //check right neighbor
        if ((cell+1) % size.x != 0 && !board[(cell +1)].visited)
        {
            neighbors.Add((cell +1));
        }

        //check left neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell -1));
        }

        return neighbors;
    }
}