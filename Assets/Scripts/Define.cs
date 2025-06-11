using UnityEngine;

public enum StateType
{
    Idle,
    Wander,
    Chase,
    Attack,
    Dead
}
public enum StatType
{
    Hp,
    Mp,
    MoveSpeed,
    Attack,
    AttackRange,
    AttackDelay,
    WanderTargetDetectRange,
    ChasingTargetDetectRange,
    DropGold,
    DropExp
}
public enum MonsterType
{
    None,
    RedCapsule,
    GreenCapsule,
    RedSquare
}
public enum Team
{
    Player,
    Monster
}
public enum UIPopupType
{
    Shop, Inventory, Equipment
}
public enum RoomType { Start, Normal, Boss }
public enum EquipmentType {None ,Weapon}
public class Define : MonoBehaviour
{
    
}
