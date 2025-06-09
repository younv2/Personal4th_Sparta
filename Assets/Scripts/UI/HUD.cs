using System;
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

    public Action<BigInteger> onGoldChanged;
    public Action<int> onStageChanged;
    public Action<int> onLevelChanged;
    public Action<float> onExpChanged;

    void OnEnable()
    {
        onGoldChanged += GoldChanged;
        onStageChanged += StageChanged;
        onLevelChanged += LevelChanged;
        onExpChanged += ExpChanged;
    }
    void OnDisable()
    {
        onGoldChanged -= GoldChanged;
        onStageChanged -= StageChanged;
        onLevelChanged -= LevelChanged;
        onExpChanged -= ExpChanged;
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
