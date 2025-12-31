using UnityEngine;
using UnityEngine.AI;

public class stalker_behaviour : MonoBehaviour
{
    public float Radious = 10f;
    private NavMeshAgent nMAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nMAgent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasArrived())
        {
            SetRandomDestination();
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
