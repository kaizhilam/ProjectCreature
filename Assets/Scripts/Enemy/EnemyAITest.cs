using UnityEngine;
using UnityEngine.AI;

public class EnemyAITest : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Vector3 targetRange;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        targetRange = (Random.insideUnitSphere * 2) + Vector3.one;
        agent.SetDestination(target.transform.position - targetRange);
    }
}
