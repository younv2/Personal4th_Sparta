using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int seed = 12345;

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
        SoundManager.Instance.PlaySound(SoundType.BGM, "bgm", true);
    }
    /// <summary>
    /// 맵 생성후 같은 프레임에 BuildNavMesh를 호출하면 Build를 못함.
    /// 한 프레임 이후 작업.
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateAndStartStage()
    {
        mapGenerator.GenerateMap();

        yield return null;

        mapGenerator.Surface.BuildNavMesh();
        StageManager.Instance.StartStage(1);
        virtualCamera.Follow = Player.transform;
        UIManager.Instance.HUD.gameObject.SetActive(true);
    }

    public void StartBuffCoroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
