using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f; // Speed at which the enemy moves towards the player

    void Start()
    {
        
    }

    void Update()
    {
        if (player != null)
        {
            // Move the enemy towards the player's position
            Vector3 direction = player.position - transform.position;
            direction.Normalize(); 

            // Move the enemy
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
