using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;

    public float sight, walkingRange, walkingSpeed, attackingSpeed;
    public bool inSight;
    public bool destinationSet;
    public LayerMask playerLayer;
    public Vector3 destinationPoint;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        RangeCheck();
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
        agent.speed = walkingSpeed;
        if (!destinationSet) //if destination is not set (which is at the start and after reaching destination) than do this
        {
            float randomX= Random.Range(-walkingRange, walkingRange);
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
        agent.speed = attackingSpeed;
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
    }
}
