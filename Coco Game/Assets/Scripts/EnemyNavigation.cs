using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;

    public float sight, walkingRange, walkingSpeed, attackingSpeed, slowDownTime;
    public float timer = 0f;
    private bool inPoop, inSight, isFleeing;
    public List<Transform> targets;
    public int targetIndex;
    public LayerMask playerLayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (isFleeing) // If the enemy is fleeing, skip the normal behavior logic.
        {
            return;
        }

        RangeCheck();
        SpeedCeck();
        //Debug.Log(agent.speed);
    }
    private void RunAway()
    {
        Vector3 fleeDirection = -(player.transform.position - transform.position).normalized;
        //Vector3 fleeTarget = transform.position + fleeDirection * walkingRange; // Use walkingRange to determine how far to flee

        agent.SetDestination(fleeDirection);
        isFleeing = true;
        StartCoroutine(ResetFleeingState(5f));
    }
    IEnumerator ResetFleeingState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFleeing = false;
    }
    public void RangeCheck()
    {
        inSight = Physics.CheckSphere(transform.position, sight, playerLayer); //creates a sphere which checks whether the enemy sees the player or not
        if (inSight) //if it sees - attack
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
        if (inPoop == false)
        {
            agent.speed = walkingSpeed;
        }
        if (targets.Count == 0) return; // Add this line to handle empty list

        agent.SetDestination(targets[targetIndex].position);
        float distance = Vector3.Distance(transform.position, targets[targetIndex].position);
        Debug.Log(distance);
        if (distance <= 1.5) // if close to the current target, find the next one
        {
            targetIndex += 1;
            if (targetIndex >= targets.Count) // Change '>' to '>=' for proper bounds checking
            {
                targetIndex = 0;
            }
            Debug.Log("OwO");
        }
    }
    public void Attacking() // attacks :)
    {
        if (inPoop == false)
        {
            agent.speed = attackingSpeed;
        }
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
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