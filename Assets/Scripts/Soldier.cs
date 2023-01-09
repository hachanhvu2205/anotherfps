using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour{
    private GameObject mEnemy;
    public float UpTime = 3.0f;
    public float ShootTime = 2.0f;
    public float DownTime = 2.0f;
    private bool mIsActive = false;
    // private Animator anim;
    private Animator mAnimator;
    // Vector3 mStartPos = Vector3.zero;
    // public NavMeshAgent agent;
    // public Transform player;
    // public LayerMask whatIsGround, whatIsPlayer;
    // public Vector3 walkPoint;
    // public GameObject projectile;
    // bool walkPointSet;
    // public float walkPointRange;
    // public float time;
    // bool alreadyAttacked;
    // public float health;
    // public float sightRange, attackRange;
    // public bool playerInSightRange, playerInAttackRange;
    // private void Awake()
    // {
    //     player = GameObject.Find("Player").transform;
    //     agent = GetComponent<NavMeshAgent>();
    // }
    
    // private void update()
    // {
    //    playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
    //     playerInAttackRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
    //     if(!playerInSightRange && !playerInAttackRange) Patroling();
    //     if(playerInSightRange && !playerInAttackRange) ChasePlayer();
    //     if(playerInSightRange && playerInAttackRange) AttackPlayer();
    // }
    // private void Patroling()
    // {
    //     if(!walkPointSet) SearchWalkPoint();
    //     if(walkPointSet) agent.SetDestination(walkPoint);
    //     Vector3 distanceToWalkPoint = transform.position-walkPoint;
    //     if(distanceToWalkPoint.magnitude<1f)
    //     {
    //         walkPointSet = false;
    //     }

    // }
    // private void SearchWalkPoint()
    // {
    //     float randomZ = Random.Range(-walkPointRange,walkPointRange);
    //     float randomx = Random.Range(-walkPointRange,walkPointRange);
    //     walkPoint = new Vector3(transform.position.x+randomx,transform.position.y+transform.position.z+randomZ);
    //     if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
    //         walkPointSet = true;
    // }
    // private void ChasePlayer()
    // {
    //     agent.SetDestination(player.position);
    // }
    // private void AttackPlayer()
    // {
    //     agent.SetDestination(transform.position);
    //     transform.LookAt(player);
    //     if(!alreadyAttacked)
    //     {
    //         Rigidbody rb = Instantiate(projectile,transform.position,Quaternion.identity).GetComponent<Rigidbody>();
    //         rb.AddForce(transform.forward*32f,ForceMode.Impulse);
    //         rb.AddForce(transform.up*8f,ForceMode.Impulse);
    //         alreadyAttacked = true;
    //         Invoke(nameof(ResetAttack),time);
    //     }
    // }
    // private void ResetAttack(){
    //     alreadyAttacked = false;
    // }
    // public void TakeDamage(int damage){
    //     health -= damage;
    //     if(health<=0) Invoke(nameof(DestroyEnemy),2f);
    // }
    // private void DestroyEnemy()
    // {
    //     Destroy(gameObject);
    // }
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position,attackRange);
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position,sightRange);
    // }
    // void Awake(){
    //     mEnemy = transform.GetChild(0).gameObject;
    //     mStartPos = mEnemy.transform.position;
    //     mAnimator = mEnemy.GetComponent<Animator>();
    // }
    // public void Activate(){
    //     mIsActive = true;
    //     mEnemy.transform.position = mStartPos;
    //     MoveUpwards();
    //     mAnimator.SetBool("shoot",true);
    //     // ShootEvent();
    //     Invoke("MoveDown",ShootTime);
    // }
    // public void ShootEvent()
    // {
    //     GameController.Instance.SetDamage(10);
    // }
    // private void MoveUpwards(){
    //     Vector3 enemyPos = mEnemy.transform.position;
    //     enemyPos.x +=2.0f;
    //     iTween.MoveTo(mEnemy,enemyPos,UpTime);
    // }
    // private void MoveDown(){
    //     mAnimator.SetBool("shoot",false);
    //     Vector3 enemyPos = mEnemy.transform.position;
    //     enemyPos.x -=2.0f;
    //     iTween.MoveTo(mEnemy,enemyPos,DownTime);
    //     iTween.MoveTo(mEnemy,iTween.Hash("x",enemyPos.x,"time",DownTime,"onComplete","OnDownComplete","onCompleteTarget",gameObject));
    
    // }
    // void OnDownComplete()
    // {
    //     mIsActive = false;
    // }
    internal void Hit()
    {
        mAnimator.SetTrigger("Die");
        
    }
    public bool IsActive{
        get{return mIsActive;}
    }
       


    
}
