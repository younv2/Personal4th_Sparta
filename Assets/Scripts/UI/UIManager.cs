using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private HUD hud;
    [SerializeField] private Button shopBtn;
    [SerializeField] private Button inventoryBtn;
    [SerializeField] private Button equipmentBtn;
    [SerializeField] private List<KeyValuePair> keyValuePairs = new List<KeyValuePair>();
    private Dictionary<UIPopupType,BasePopup> popups =new();
    public HUD HUD { get { return hud; } }

    protected override void Awake()
    {
        base.Awake();
        foreach(var popup in keyValuePairs)
        {
            popups.Add(popup.key,popup.value);
        }
        foreach(var popup in popups.Values)
        {
            popup.Initialize();
        }
        shopBtn.onClick.AddListener(() => popups[UIPopupType.Shop].Show());
        inventoryBtn.onClick.AddListener(() => popups[UIPopupType.Inventory].Show());
        equipmentBtn.onClick.AddListener(() => popups[UIPopupType.Equipment].Show());
    }
}
[System.Serializable]
public struct KeyValuePair
{
    public UIPopupType key;
    public BasePopup value;
}