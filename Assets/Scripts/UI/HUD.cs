using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private TextMeshProUGUI stageTxt;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private Image expBar;

    private PlayerStat playerStat;
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        goldTxt.text = gameManager.Inventory.Gold.ToString();
        stageTxt.text = StageManager.Instance.CurrentStage.ToString();
        playerStat = gameManager.Player.Stat as PlayerStat;
        levelTxt.text = playerStat.Level.ToString();
        gameManager.Inventory.onGoldChanged += GoldChanged;
        StageManager.Instance.onStageChanged += StageChanged;
        playerStat.onLevelChanged += LevelChanged;
        playerStat.onExpChanged += ExpChanged;
    }
    private void OnDisable()
    {
        if(gameManager != null)
            gameManager.Inventory.onGoldChanged -= GoldChanged;
        if(StageManager.Instance != null)
            StageManager.Instance.onStageChanged -= StageChanged;
        playerStat.onLevelChanged -= LevelChanged;
        playerStat.onExpChanged -= ExpChanged;
    }
    public void GoldChanged(BigInteger value)
    {
        goldTxt.text = value.ToString();
    }
    public void StageChanged(int value)
    {
        stageTxt.text = value.ToString();
    }
    public void ExpChanged(float value)
    {
        expBar.fillAmount = Mathf.Clamp01(value);
    }
    public void LevelChanged(int level)
    {
        levelTxt.text = level.ToString();
    }
}
