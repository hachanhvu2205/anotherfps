using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject Player;
    public float Distance;

    public bool isAngered;

    public NavMeshAgent _agent;

    void Start()
    {

    }

    void Update()
    {
        Distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if(Distance <=50)
        {
            isAngered = true;
        }

        if(Distance > 50f)
        {
            isAngered = false;
        }

        if(isAngered)
        {
            _agent.isStopped = false;
            _agent.SetDestination(Player.transform.position);
        }

        if(!isAngered)
        {
            _agent.isStopped = true;
        }
    }

    
 
}
