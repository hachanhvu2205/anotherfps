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

        if(GameManager.Instance.state == GameState.Fight)
        {
            // Change to Move towards Player and Shoot
            _agent.isStopped = true;
        } else if (GameManager.Instance.state == GameState.Escape)
        {
            _agent.isStopped = false;
            _agent.SetDestination(Player.transform.position);
        }
        
    }

    
 
}
