using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAIController : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected BaseStat stat;
    protected Vector3 origin;
    protected Animator animator;
    public bool IsStopped => !agent.pathPending && (!agent.hasPath || agent.remainingDistance <= 0.2f);
    public BaseStat Stat => stat;

    protected virtual void Awake()
    {
        origin = transform.position;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    public void Init(BaseStat stat)
    {
        this.stat = stat;
    }
    public void SetSpeed(StateType state)
    {
        animator?.SetFloat("Speed", agent.speed);
    }
    public void MoveTo(Vector3 destination)
    {
        agent.isStopped = false;
        agent.SetDestination(destination);
        
    }
    public bool MoveToRandom()
    {
        Vector3 random = transform.position + Random.insideUnitSphere * 5f;
        random.y = transform.position.y;

        if (NavMesh.SamplePosition(random, out var hit, 2.0f, NavMesh.AllAreas))
        {
            MoveTo(hit.position);
            return true;
        }

        return false;
    }
    public void MoveToOrigin()
    {
        MoveTo(origin);
    }

    public virtual void StopMoving()
    {
        agent.ResetPath();
        agent.isStopped = true;
    }

    public void LookAt(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        dir.y = 0f;                      
        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = targetRot;
    }

    public void PlayDamagedAnimation()
    {
        animator.SetTrigger("Hit");
    }

    public void PlayDieAnimation()
    {
        animator.SetTrigger("Dead");
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSecondsRealtime(2f);
        MonoBehaviour.Destroy(gameObject);
    }
}