using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityFactory : MonoBehaviour
{
    [System.Serializable]
    public class EntityEntry
    {
        public string id; 
        public GameObject prefab; 
    }
    [SerializeField] private List<EntityEntry> entityPrefabs;

    private Dictionary<string, GameObject> prefabMap;

    private void Awake()
    {
        prefabMap = new Dictionary<string, GameObject>();
        foreach (var entry in entityPrefabs)
        {
            if (!prefabMap.ContainsKey(entry.id))
                prefabMap.Add(entry.id, entry.prefab);
        }
    }
    private void Start()
    {
        var triangulation = NavMesh.CalculateTriangulation();

        bool navMeshExists = triangulation.vertices != null &&
                             triangulation.vertices.Length > 0 &&
                             triangulation.indices != null &&
                             triangulation.indices.Length > 0;
        if(!navMeshExists)
        {
            Debug.LogError("NavMesh가 존재하지 않습니다");
            return;
        }
        Transform monsterParent = new GameObject("MonsterParent").transform;
        Transform AnimalParent = new GameObject("AnimalParent").transform;
        
        for (int i =0;i<30;i++)
        {
            Create("Deer", GetRandomPointOnNavMesh(), Quaternion.identity,AnimalParent);
            Create("Skeleton", GetRandomPointOnNavMesh(), Quaternion.identity,monsterParent);
        }
        
    }
    public static Vector3 GetRandomPointOnNavMesh()
    {
        var navMesh = NavMesh.CalculateTriangulation();
        int triIndex = Random.Range(0, navMesh.indices.Length / 3) * 3;

        Vector3 a = navMesh.vertices[navMesh.indices[triIndex]];
        Vector3 b = navMesh.vertices[navMesh.indices[triIndex + 1]];
        Vector3 c = navMesh.vertices[navMesh.indices[triIndex + 2]];

        // 삼각형 내 임의의 위치 뽑기
        float r1 = Mathf.Sqrt(Random.value);
        float r2 = Random.value;
        Vector3 point = (1 - r1) * a + r1 * (1 - r2) * b + r1 * r2 * c;

        return point;
    }
    public bool RandomPointOnNavMesh(Vector3 center, float radius, out Vector3 result, int areaMask = NavMesh.AllAreas, int maxTries = 10)
    {

        for (int i = 0; i < maxTries; i++)
        {
            Vector3 random = center + Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(random, out var hit, 1.0f, areaMask))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    public GameObject Create(string id, Vector3 pos, Quaternion rot,Transform parent)
    {
        if (!prefabMap.TryGetValue(id, out var prefab)) 
            return null;

        var go = Instantiate(prefab, pos, rot,parent);
        var entity = go.GetComponent<EntityBase>();
        entity?.Init();

        return go;
    }
}