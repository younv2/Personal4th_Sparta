using System.Numerics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private BigInteger gold;


    public void AddGold(BigInteger gold)
    {
        this.gold += gold;
        UIManager.Instance.HUD.onGoldChanged?.Invoke(this.gold);
    }
    public bool SpendGold(BigInteger gold)
    {
        if(this.gold < gold)
        {
            return false;
        }
        this.gold -= gold;
        UIManager.Instance.HUD.onGoldChanged?.Invoke(this.gold);
        return true;
    }
}
