using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class walkingEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float stopDistance = 2.0f;
    NavMeshAgent agent;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            animator.SetBool("walk",false);
            animator.SetBool("attack",false);
            return;
        }
        //print("distance="+Vector3.Distance(transform.position, target.position));
        if (Vector3.Distance(transform.position,target.position)<=stopDistance)
        {
            animator.SetBool("walk", false);
            animator.SetBool("attack", true);
            return;
        }
        animator.SetBool("walk", true);
        animator.SetBool("attack", false);
        agent.SetDestination(target.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            target = col.transform;
        }
    }
}
