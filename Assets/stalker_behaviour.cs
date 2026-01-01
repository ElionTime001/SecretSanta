using UnityEngine;
using UnityEngine.AI;

public class stalker_behaviour : MonoBehaviour
{
    public float Radious = 10f;
    public float attackRange = 15f;
    private NavMeshAgent nMAgent;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nMAgent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Vector3.Distance (this.transform.position, target.position) < attackRange)
        {
            nMAgent.speed = 5;
            NavMeshHit hit;
            NavMesh.SamplePosition(target.position, out hit, 2f, 1);
            nMAgent.SetDestination(hit.position);
            Debug.Log(Vector3.Distance (this.transform.position, target.position));

        }
        else
        {
            nMAgent.speed = 2; //should not be hardcoded 
            if (HasArrived())
            {
                SetRandomDestination();
            }
        }
        // Debug.Log(GetRandomPoint() );
    }

    bool HasArrived()
    {
        return nMAgent.remainingDistance <= nMAgent.stoppingDistance;
    }

    private Vector3 GetRandomPoint() //gets random point on navmesh in circle
    {
        Vector3 randomDirection = Random.insideUnitCircle*Radious;
        Vector3 randomPoint = transform.position + randomDirection;
        NavMeshHit hit;
        Vector3 finalPosition = transform.position;
        if(NavMesh.SamplePosition(randomPoint, out hit, 2f, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    
    void SetRandomDestination()
    {
        nMAgent.SetDestination(GetRandomPoint());
    }
}
