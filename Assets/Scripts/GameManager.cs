using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int seed = 12345;

    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private LevelTableSO levelTable;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCamera;
    public LevelTableSO LevelTable {  get { return levelTable; } }
    public Inventory Inventory { get; private set; } = new();
    public Player Player { get; set; } = new();
    void Start()
    {
        UnityEngine.Random.InitState(seed);
        StartCoroutine(GenerateAndStartStage());
    }
    /// <summary>
    /// �� ������ ���� �����ӿ� BuildNavMesh�� ȣ���ϸ� Build�� ����.
    /// �� ������ ���� �۾�.
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateAndStartStage()
    {
        mapGenerator.GenerateMap();

        yield return null;

        mapGenerator.Surface.BuildNavMesh();
        StageManager.Instance.StartStage(1);
        virtualCamera.Follow = Player.transform;
    }
}
