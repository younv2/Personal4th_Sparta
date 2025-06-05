using System.Collections;
using UnityEngine;

public class PlayerController : BaseAIController
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
    private void Update()
    {
        CurrentTarget = MonsterManager.Instance.GetNearestMonster(transform.position);
    }
    IEnumerator Attack(Transform target)
    {
        while (true)
        {
            Debug.Log("공격!");
            if (stat.TryGetStat(StatType.AttackDelay, out var data))
            {
                yield return new WaitForSeconds(data.FinalValue);
            }
        }
    }
}