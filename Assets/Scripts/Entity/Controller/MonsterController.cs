using System.Collections;
using UnityEngine;

public class MonsterController : BaseAIController
{
    Coroutine coroutine;
    private void Start()
    {
        CurrentTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityBase>();
    }
    public void StartAttack(Transform target)
    {
        coroutine = StartCoroutine(Attack(target));
    }
    public void StopAttack() 
    {
        StopCoroutine(coroutine);
    }
    IEnumerator Attack(Transform target)
    {
        while (true)
        {
            Debug.Log("공격!");
            if(stat.TryGetStat(StatType.AttackDelay,out var data))
            {
                yield return new WaitForSeconds(data.FinalValue);
            }
        }
    }
}