using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [Header("Seed")]
    public int seed = 12345;

    [Header("Room Prefabs")]
    public GameObject startRoom;
    public GameObject[] normalRooms;
    public GameObject bossRoom;

    [Header("Settings")]
    public int maxRoomCount = 10;
    public float cellSize = 20f;            // 한 셀(방) 크기
    [Range(0f, 1f)]
    public float exitConnectionChance = 0.6f;

    [Header("Debug")]
    public bool drawGizmos = false;

    // === 내부 상태 ===
    private readonly HashSet<Vector2Int> placedCells = new();                                  // 이미 사용된 셀 좌표
    private readonly List<GameObject> spawnedRooms = new();                                    // 생성된 방 프리팹
    private readonly HashSet<(Vector2Int cell, Dir dir)> connected = new();                    // (셀, 방향) 단위로 연결 여부

    private enum Dir { North, South, East, West }

    private static readonly Dictionary<Dir, Vector2Int> dirOffset = new()
    {
        {Dir.North, new Vector2Int(0,  1)},
        {Dir.South, new Vector2Int(0, -1)},
        {Dir.East,  new Vector2Int(1,  0)},
        {Dir.West,  new Vector2Int(-1, 0)}
    };

    // --------------------------------------------------
    // 초기 진입
    // --------------------------------------------------
    void Start()
    {
        Random.InitState(seed);
        GenerateDungeon();
    }

    // --------------------------------------------------
    // 던전 생성 주루틴
    // --------------------------------------------------
    void GenerateDungeon()
    {
        var queue = new Queue<RoomNode>();

        // 1) 시작 방 배치 (0,0)
        Vector2Int startCell = Vector2Int.zero;
        GameObject start = CreateRoom(startRoom, startCell);
        queue.Enqueue(new RoomNode(start, startCell));

        int roomCount = 1;

        // 2) BFS 확장
        while (queue.Count > 0 && roomCount < maxRoomCount)
        {
            var current = queue.Dequeue();
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

                if (placedCells.Contains(toCell)) continue;          // 이미 방 존재

                bool enqueue = true;
                GameObject prefab;

                if (roomCount == maxRoomCount - 1)
                {
                    prefab = bossRoom;
                    enqueue = false;  // 보스방은 확장 금지
                }
                else
                {
                    prefab = normalRooms[Random.Range(0, normalRooms.Length)];
                }

                GameObject newRoom = CreateRoom(prefab, toCell);

                // 연결 정보 기록 (양방향)
                connected.Add((fromCell, dir));
                connected.Add((toCell, Opposite(dir)));

                if (enqueue)
                {
                    queue.Enqueue(new RoomNode(newRoom, toCell));
                }
                else
                {
                    return; // 보스방 배치 후 즉시 종료
                }

                roomCount++;
                if (roomCount >= maxRoomCount) break;
            }
        }
    }

    // --------------------------------------------------
    // 방 생성 & 등록
    // --------------------------------------------------
    GameObject CreateRoom(GameObject prefab, Vector2Int cell)
    {
        Vector3 worldPos = new(cell.x * cellSize, 0f, cell.y * cellSize);
        GameObject room = Instantiate(prefab, worldPos, Quaternion.identity, transform);
        spawnedRooms.Add(room);
        placedCells.Add(cell);
        return room;
    }

    // --------------------------------------------------
    // 방향 계산 & 보조 함수
    // --------------------------------------------------
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

    // --------------------------------------------------
    // 내부 데이터 구조체
    // --------------------------------------------------
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

    // --------------------------------------------------
    // 디버그용 기즈모
    // --------------------------------------------------
    void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.green;
        foreach (var cell in placedCells)
        {
            Vector3 pos = new(cell.x * cellSize, 0f, cell.y * cellSize);
            Gizmos.DrawWireCube(pos, Vector3.one * cellSize * 0.9f);
        }
    }
}
