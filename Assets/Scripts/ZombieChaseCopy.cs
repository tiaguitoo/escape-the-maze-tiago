using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseCopy : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool playerCaught = false;

    public float chaseDistance = 10f;
    public float startDistance = 0.001f;
    private float timer;
    private Vector3 startPosition;
    private bool isChasing;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        startPosition = navMeshAgent.transform.position;
        isChasing = false;
    }

    void Update()
    {
        if (!playerCaught)
        {
            float distanceToPlayer = Vector3.Distance(navMeshAgent.transform.position, player.position);

            if (distanceToPlayer <= chaseDistance && !IsWallBetweenPlayer())
            {
                isChasing = true;
                timer = 0f;
                navMeshAgent.SetDestination(player.position);
                animator.SetBool("isMoving", true);
            }
            else
            {
                if(isChasing){
                    timer += Time.deltaTime;
                    if (timer >= 10f)
                    {
                        RetrackToStartingPosition();
                    }
                    else
                    {
                        navMeshAgent.ResetPath();
                        animator.SetBool("isMoving", false);
                    }
                }
                else{
                    // navMeshAgent.ResetPath();
                    animator.SetBool("isMoving", false);
                }
            }
        }
    }

    private bool IsWallBetweenPlayer()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.position - navMeshAgent.transform.position;

        if (Physics.Raycast(navMeshAgent.transform.position, directionToPlayer, out hit, chaseDistance))
        {
            if (hit.transform != player)
            {
                // Hit something other than the player, assuming it's a wall
                return true;
            }
        }
        return false;
    }

    private void RetrackToStartingPosition()
    {
        float distanceToStart = Vector3.Distance(navMeshAgent.transform.position, startPosition);
        if (distanceToStart>=startDistance)
        {
            navMeshAgent.SetDestination(startPosition);
            animator.SetBool("isMoving", true);
        }
        else
        {
            isChasing = false;
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         playerCaught = true;
    //     }
    // }
}
