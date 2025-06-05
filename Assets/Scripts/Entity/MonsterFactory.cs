using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoSingleton<MonsterFactory>
{
    [System.Serializable]
    public class MonsterEntry
    {
        public MonsterType id; 
        public GameObject prefab; 
    }
    [SerializeField] private List<MonsterEntry> entityPrefabs;

    private Dictionary<MonsterType, GameObject> prefabMap = new();

    private void Awake()
    {
        foreach (var entry in entityPrefabs)
        {
            if (!prefabMap.ContainsKey(entry.id))
                prefabMap.Add(entry.id, entry.prefab);
        }
    }

    public GameObject Create(MonsterType id, Vector3 pos, Quaternion rot,Transform parent = null)
    {
        if (!prefabMap.TryGetValue(id, out var prefab)) 
            return null;

        var go = Instantiate(prefab, pos, rot,parent);
        var entity = go.GetComponent<Monster>();
        entity?.Init();
        MonsterManager.Instance.Register(entity);
        return go;
    }
}