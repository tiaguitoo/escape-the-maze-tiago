using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMonsterChase : MonoBehaviour
{
    private Transform player;
    public Transform  dest1, dest2;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    int randNumber;
    private bool walking, chasing, idle;
    private Vector3 dest;
    public float walkingSpeed, runningSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        walking = true;
        randNumber = Random.Range(0,2);
        if(randNumber==0)
            dest = dest1.position;
        if(randNumber==1)
            dest = dest2.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(walking)
        {
            navMeshAgent.destination = dest;
            navMeshAgent.speed = walkingSpeed;
        }
        if(chasing)
        {
            dest = player.position;
            navMeshAgent.destination = dest;
            navMeshAgent.speed = runningSpeed;
        }
        if(idle)
        {
            navMeshAgent.speed = 0f;
        }
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            chasing = true;
            walking = false;
            idle = false;
            animator.ResetTrigger("isIdle");
            animator.ResetTrigger("isWalking");
            animator.SetTrigger("isRunning");
            StopCoroutine(Chase());
            StartCoroutine(Chase());
        }
        if(other.CompareTag("Destination"))
        {
            if(!chasing){
                walking = false;
                idle = true;
                animator.ResetTrigger("isWalking");
                animator.SetTrigger("isIdle");
                StartCoroutine(NextDestination());
            }
        }
    }

    IEnumerator NextDestination(){
        yield return new WaitForSeconds(3f);
        idle = false;
        walking = true;
        animator.ResetTrigger("isIdle");
        animator.SetTrigger("isWalking");
        randNumber = Random.Range(0,2);
        if(randNumber==0)
            dest = dest1.position;
        if(randNumber==1)
            dest = dest2.position;
    }

    IEnumerator Chase(){
        yield return new WaitForSeconds(3f);
        chasing = false;
        walking = true;
        animator.ResetTrigger("isIdle");
        animator.ResetTrigger("isRunning");
        animator.SetTrigger("isWalking");
        randNumber = Random.Range(0,2);
        if(randNumber==0)
            dest = dest1.position;
        if(randNumber==1)
            dest = dest2.position;
    }
}
