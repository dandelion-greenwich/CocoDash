using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public GameObject kennel;
    public Vector3 spawnPoint;
    public GameObject player;
    public int health;
    private GameObject heart1;
    private GameObject heart2;
    private GameObject heart3;
    AbilitiesScript abilitiesScript;
    CocoUI cocoUI;
    int deathCount; // to keep track of how many times the player dies - D'Arcy

    public GameOver gameOverManager;
    private bool isDead;

    private void Awake()
    {
        heart1 = GameObject.Find("Heart1");
        heart2 = GameObject.Find("Heart2");
        heart3 = GameObject.Find("Heart3"); // all three lives are visible from the start of the game - D'Arcy
    }


    void Start()
    {
        spawnPoint = gameObject.transform.position; // sets the default respawn point as our original spawn point - D'Arcy
        abilitiesScript = GetComponent<AbilitiesScript>();
        cocoUI = GameObject.FindGameObjectWithTag("UI").GetComponent<CocoUI>();
    }

    private void OnTriggerEnter(Collider other) //every time player collides with another rigid body, these 'if' statements happens - D'Arcy
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            spawnPoint = player.transform.position;
            if (abilitiesScript.allTreatsCollected && cocoUI != null)
            {
                cocoUI.CheckGameState(CocoUI.GameState.Victory);
            }
            // makes the checkpoint the most recent kennel the player has collided with - D'Arcy
        }
        if (other.gameObject.tag == "Enemy")
        {
            GameManager.health -= 1;
            player.transform.position = spawnPoint; // respawns the player when they collide with the enemy - D'Arcy
            deathCount += 1; // increases death count by 1 so that the 'if' statements have something to refer to - D'Arcy
        }
        if (other.gameObject.tag == "KillZone")
        {
            player.transform.position = spawnPoint; // respawns the player if they fall off the map (changed this to empty game object) - D'Arcy
            deathCount += 1;
        }
        if (deathCount == 1)
        {
            Destroy(heart1);
        }
        if(deathCount == 2)
        {
            Destroy(heart2);
        }
        if(deathCount == 3 && !isDead)
        {
            Destroy(heart3); // destroys one heart from left to right in UI each time the player dies and respawns - D'Arcy
            isDead = true;
            gameOverManager.gameOver();
        }
    }
}
