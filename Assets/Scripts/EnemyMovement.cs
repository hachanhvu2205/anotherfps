using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
//    public GameObject Player;
//     public float Distance;

//     public bool isAngered;

    public NavMeshAgent navMeshAgent;
    // public NavMeshAgent _agent;
    private Animator mAnimator;
    [SerializeField]Transform target;
    [SerializeField]float chaseRange = 5f;
    [SerializeField]float turnSpeed = 5f;
       float distanceToTarget = Mathf.Infinity;
       public float timeRemaining = 10;
    bool isProvoked = false;
    bool haveEngaged = false;


    void Start()
    {
  navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(haveEngaged){
            if(distanceToTarget >= 10f && timeRemaining >0){
            timeRemaining-=Time.deltaTime;
            Debug.Log(timeRemaining.ToString());
        } else if(distanceToTarget <=10f && timeRemaining>0){
            timeRemaining=10;
            Debug.Log("Reset");
        }
        }
        
         if(isProvoked && GetComponent<AIHealth>().alive ) {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange) {
            isProvoked = true;
        }
     
        // Distance = Vector3.Distance(Player.transform.position, this.transform.position);

        // if(Distance <=5f)
        // {
        //     isAngered = true;
        // }

        // if(Distance > 5f)
        // {
        //     isAngered = false;
        // }

        // if(isAngered)
        // {
        //     // _agent.isStopped = false;
        //     // mAnimator.SetTrigger("run");
        //     GetComponent<Animator>().SetBool("Idle",false);
        // GetComponent<Animator>().SetTrigger("Move");
        //     _agent.SetDestination(Player.transform.position);
        // }

        // if(!isAngered)
        // {
        //     // _agent.isStopped = true;
        //       GetComponent<Animator>().SetBool("Move", false);
        // GetComponent<Animator>().SetTrigger("Idle");
        // }

    }
void EngageTarget() {
    haveEngaged = true;
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance) {
            ChaseTarget();
        }
        else if(distanceToTarget <= navMeshAgent.stoppingDistance) {
            AttackTarget();
        } 
    }

    void ChaseTarget() {
       GetComponent<Animator>().SetBool("Idle",true);
       GetComponent<Animator>().SetBool("Die",false);
        GetComponent<Animator>().SetTrigger("Move");
        
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget() {
          GetComponent<Animator>().SetBool("Move", false);
        GetComponent<Animator>().SetTrigger("Idle");
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    
 
}
