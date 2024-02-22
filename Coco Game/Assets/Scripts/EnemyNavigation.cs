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
        inSight = Physics.CheckSphere(transform.position, sight, playerLayer);
        if (inSight)
        {
            Attacking();
        }
        else
        {
            Walking();
        }
    }
    public void Walking()
    {
        agent.speed = walkingSpeed;
        if (!destinationSet)
        {
            float randomX= Random.Range(-walkingRange, walkingRange);
            float randomZ = Random.Range(-walkingRange, walkingRange);
            destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(destinationPoint, -transform.up, 100f))
            {
                destinationSet = true;
            }
        }
        else if (destinationSet)
        {
            agent.SetDestination(destinationPoint);
        }

        float distance = Vector3.Distance(transform.position, destinationPoint);
        if (distance <= 1)
        {
            destinationSet = false;
        }
    }
    public void Attacking()
    {
        agent.speed = attackingSpeed;
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
    }
}
