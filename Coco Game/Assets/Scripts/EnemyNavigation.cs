using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;

    public float sight, walkingRange, walkingSpeed, attackingSpeed, slowDownTime, fovSight, angle;
    public float timer = 0f;
    private bool inPoop, inSight, fovInSight, isFleeing;
    public List<Transform> targets;
    public int targetIndex;
    public LayerMask playerLayer;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (isFleeing) // If the enemy is fleeing, skip the normal behavior logic.
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false); // Make sure we're not walking or running while fleeing
            return;
        }

        RangeCheck();
        SpeedCeck();
        //Debug.Log(agent.speed);
        //Debug.Log(inSight);
    }
    private void RunAway()
    {
        Vector3 fleeDirection = player.transform.position - transform.forward * 100;
        //Vector3 fleeTarget = transform.position + fleeDirection * walkingRange; // Use walkingRange to determine how far to flee
        //Debug.Log(fleeDirection);
        agent.SetDestination(fleeDirection);
        isFleeing = true;
        StartCoroutine(ResetFleeingState(5f));

        animator.SetBool("isRunning", true); 
        animator.SetBool("isWalking", false);
    }
    IEnumerator ResetFleeingState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFleeing = false;
        animator.SetBool("isRunning", false);
    }
    public void RangeCheck()
    {
        inSight = Physics.CheckSphere(transform.position, sight, playerLayer); //creates a sphere which checks whether the enemy sees the player or not

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, fovSight, playerLayer);
        if (rangeChecks.Length > 0)
        {
            Vector3 directonToPlayer = (player.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directonToPlayer) < angle/2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (Physics.Raycast(transform.position, directonToPlayer, distanceToPlayer))
                {
                    fovInSight = true;
                    //Debug.Log("raycast true");
                }
                else
                {
                    fovInSight = false;
                    //Debug.Log("raycast false");
                }
            }
            else
            {
                fovInSight = false;
            }
        }

        if (inSight || fovInSight) //if it sees - attack
        {
            Attacking();
        }
        else //if not - just walk around
        {
            Walking();
        }
    }
    public void Walking()
    {
        if (targets.Count == 0)
        {
            animator.SetBool("isWalking", true); 
            animator.SetBool("isRunning", false); 
            return;
        }

        if (inPoop == false)
        {
            agent.speed = walkingSpeed;
        }

        animator.SetBool("isWalking", true); // Enemy starts walking

        agent.SetDestination(targets[targetIndex].position);
        float distance = Vector3.Distance(transform.position, targets[targetIndex].position);
        //Debug.Log(distance);
        if (distance <= 1.5)
        {
            targetIndex += 1;
            if (targetIndex >= targets.Count)
            {
                targetIndex = 0;
            }
            //Debug.Log("OwO");
        }
    }
    public void Attacking() // attacks :)
    {
        if (inPoop == false)
        {
            agent.speed = attackingSpeed;
        }
        agent.SetDestination(player.transform.position);
        animator.SetBool("isRunning", true); 
        animator.SetBool("isWalking", false);
        //transform.LookAt(new Vector3(player.transform.position.x, 0.5f, player.transform.position.z));
        //transform.localEulerAngles = new Vector3(0.5f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    public void SpeedCeck()
    {
        if (inPoop == true)
        {
            timer += Time.deltaTime;
            agent.speed = 2f;
        }

        if (timer >= slowDownTime)
        {
            timer = 0f;
            inPoop = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Poop")
        {
            inPoop = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barking")
        {
            RunAway();
        }
    }
}