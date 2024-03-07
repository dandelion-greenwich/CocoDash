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
    private bool destinationSet, inPoop, inSight, isFleeing;
    public LayerMask playerLayer;
    private Vector3 destinationPoint;

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
        Vector3 fleeDirection = (transform.position - player.transform.position).normalized;
        Vector3 fleeTarget = transform.position + fleeDirection * walkingRange; // Use walkingRange to determine how far to flee

        agent.SetDestination(fleeTarget);
        isFleeing = true;
        StartCoroutine(ResetFleeingState(5f)); 
    }
    IEnumerator ResetFleeingState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFleeing = false;
        destinationSet = false;
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
        if (!destinationSet && !isFleeing) //if destination is not set (which is at the start and after reaching destination) than do this
        {
            float randomX = Random.Range(-walkingRange, walkingRange);
            float randomZ = Random.Range(-walkingRange, walkingRange);
            destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); //creates random destination point at map

            if (Physics.Raycast(destinationPoint, -transform.up, 100f)) //checks a destination point
            {
                destinationSet = true;
            }
        }
        else if (destinationSet)
        {
            agent.SetDestination(destinationPoint);
        }

        float distance = Vector3.Distance(transform.position, destinationPoint);
        if (distance <= 1) // checks if distance to a point less than one, than destination set is true so it would look for a new destination
        {
            destinationSet = false;
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