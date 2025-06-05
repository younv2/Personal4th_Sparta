using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/FSM/StateSet")]
public class StateSetSO : ScriptableObject
{
    public StateType[] stateTypes;

    public bool Contains(StateType t) => System.Array.Exists(stateTypes, x => x == t);
}