using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    protected BaseAIController controller;
    protected BaseStat stat;
    [SerializeField] protected StatDataSO statDataSO;
    public void Init()
    {
    }

}
