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
    private bool inPoop, inSight;
    public LayerMask playerLayer;
    public List<Transform> targets;
    public int targetIndex;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        RangeCheck();
        SpeedCeck();
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
        agent.SetDestination(targets[targetIndex].position);
        float distance = Vector3.Distance(transform.position, targets[targetIndex].position);
        Debug.Log(distance);
        if (distance <= 1.5) // checks if distance to a point less than one, than destination set is true so it would look for a new destination
        {
            targetIndex += 1;
            if (targetIndex > targets.Count - 1)
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
}
