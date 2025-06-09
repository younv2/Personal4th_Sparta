using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private HUD hud;
    public HUD HUD { get { return hud; } }

    
}
