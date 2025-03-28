using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent navMeshAgent;

    public void SetUp()
    {
        //this.target = target;
        target = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
    private void Awake()
    {
        SetUp();
    }

    private void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }
}
