using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f; // Speed at which the enemy moves towards the player
    //public float acceleration = 0.1f; 
    //public float maxSpeed = 10.0f; // Maximum speed

    void Update()
    {
        if (player != null)
        {
            // Move the enemy towards the player's position
            Vector3 direction = player.position - transform.position;
            direction.Normalize(); 

            // Move the enemy
            transform.position += direction * speed * Time.deltaTime;

            // Increase the speed with time, without exceeding the maximum speed
           // speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
        }
    }
}
