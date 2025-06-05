using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class MonsterController : BaseAIController
{
    Coroutine coroutine;
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
            animator?.SetTrigger("Attack");
            if(stat.TryGetStat(StatType.AttackDelay,out var data))
            {
                yield return new WaitForSeconds(data.FinalValue);
            }
        }
    }
}