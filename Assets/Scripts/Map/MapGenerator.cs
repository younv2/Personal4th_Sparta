using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    public NavMeshSurface Surface {  get { return surface; } }
    [Header("Room Prefabs")]
    public GameObject startRoom;
    public GameObject[] normalRooms;
    public GameObject bossRoom;

    [Header("Settings")]
    public int maxRoomCount = 10;
    public float cellSize = 10f;       
    [Range(0f, 1f)]
    public float exitConnectionChance = 0.6f;

    private readonly HashSet<Vector2Int> placedCells = new();                            
    private readonly List<GameObject> spawnedRooms = new();                              
    private readonly HashSet<(Vector2Int cell, Dir dir)> connected = new();              

    private enum Dir { North, South, East, West }

    

    private static readonly Dictionary<Dir, Vector2Int> dirOffset = new()
    {
        {Dir.North, new Vector2Int(0,  1)},
        {Dir.South, new Vector2Int(0, -1)},
        {Dir.East,  new Vector2Int(1,  0)},
        {Dir.West,  new Vector2Int(-1, 0)}
    };


    /// <summary>
    /// 맵 생성
    /// </summary>
    public void GenerateMap()
    {
        var queue = new Queue<RoomNode>();

        // 1) 시작 방 배치 (0,0)
        Vector2Int startCell = Vector2Int.zero;
        GameObject start = CreateRoom(startRoom, startCell);
        queue.Enqueue(new RoomNode(start, startCell));

        int roomCount = 1;

        // 2) BFS
        while (queue.Count > 0 && roomCount < maxRoomCount)
        {
            RoomNode current = queue.Dequeue();
            var exits = current.room.GetComponent<Room>().exits.OrderBy(_ => Random.value);

            foreach (var exit in exits)
            {
                if (Random.value > exitConnectionChance) continue;   // 확률로 연결 스킵

                Dir dir = GetDir(exit.forward);
                Vector2Int fromCell = current.cell;
                Vector2Int toCell = fromCell + dirOffset[dir];

                // 중복·역방향 체크
                if (connected.Contains((fromCell, dir)) || connected.Contains((toCell, Opposite(dir))))
                    continue;

                if (placedCells.Contains(toCell)) continue; 

                bool isBossRoom = true;
                GameObject prefab;

                if (roomCount == maxRoomCount - 1)
                {
                    prefab = bossRoom;
                    isBossRoom = false;
                }
                else
                {
                    prefab = normalRooms[Random.Range(0, normalRooms.Length)];
                }

                GameObject newRoom = CreateRoom(prefab, toCell);
                StageManager.Instance.allRooms.Add(new RoomArea(newRoom.GetComponent<Room>().roomType,newRoom.GetComponent<Collider>().bounds));
                // 연결 정보 기록 (양방향)
                connected.Add((fromCell, dir));
                connected.Add((toCell, Opposite(dir)));

                if (isBossRoom)
                {
                    queue.Enqueue(new RoomNode(newRoom, toCell));
                }
                else
                {
                    return;
                }

                roomCount++;
                if (roomCount >= maxRoomCount) break;
            }
        }
        
    }

    /// <summary>
    ///  방 생성
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="cell"></param>
    /// <returns></returns>
    GameObject CreateRoom(GameObject prefab, Vector2Int cell)
    {
        Vector3 worldPos = new(cell.x * cellSize, 0f, cell.y * cellSize);
        GameObject room = Instantiate(prefab, worldPos, Quaternion.identity, transform);
        spawnedRooms.Add(room);
        placedCells.Add(cell);
        return room;
    }


    static Dir GetDir(Vector3 forward)
    {
        Vector3 dir = forward.normalized;
        if (Vector3.Dot(dir, Vector3.forward) > 0.707f) return Dir.North;
        if (Vector3.Dot(dir, Vector3.back) > 0.707f) return Dir.South;
        if (Vector3.Dot(dir, Vector3.right) > 0.707f) return Dir.East;
        return Dir.West;
    }

    static Dir Opposite(Dir d) => d switch
    {
        Dir.North => Dir.South,
        Dir.South => Dir.North,
        Dir.East => Dir.West,
        _ => Dir.East
    };

    struct RoomNode
    {
        public GameObject room;
        public Vector2Int cell;
        public RoomNode(GameObject room, Vector2Int cell)
        {
            this.room = room;
            this.cell = cell;
        }
    }
}
