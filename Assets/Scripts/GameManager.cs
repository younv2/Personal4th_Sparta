public class GameManager : MonoSingleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        StageManager.Instance.StartStage(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
