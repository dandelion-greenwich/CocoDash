using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public GameObject kennel;
    public Vector3 spawnPoint;
    public GameObject player;


    void Start()
    {
        spawnPoint = gameObject.transform.position; // sets the respawn point as our original spawn point - D'Arcy
    }

    private void OnTriggerEnter(Collider other) //every time player collides with another rigid body, these 'if' statements happens - D'Arcy
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            spawnPoint = player.transform.position; // makes the checkpoint the most recent kennel the player has collided with - D'Arcy
        }
        if (other.gameObject.tag == "Enemy")
        {
            print("ENTER"); // testing code to see if the collision works - D'Arcy
            /*Destroy(gameObject);*/ // destroys the player when enemy collides with them - D'Arcy
            player.transform.position = spawnPoint; // respawns the player when they collide with the enemy - D'Arcy
        }
        if (other.gameObject.tag == "KillZone")
        {
            player.transform.position = spawnPoint; // respawns the player if they fall off the map (changed this to empty game object) - D'Arcy
        }
    }
}
