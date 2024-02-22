using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject kennel;
    //Vector3 spawnPoint;
    [SerializeField] float dead;
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] GameObject player;

    void Start()
    {
        spawnPoint = gameObject.transform.position; // sets the original spawn point to the same place as our player - D'Arcy
    }
    // Start is called before the first frame update

    void Update()
    {

        if (gameObject.transform.position.y < -dead) // falling off the map kills player and sends them back to the spawn point - D'Arcy
        {
            //gameObject.transform.position = spawnPoint;
            player.transform.position = spawnPoint; // could change this part to empty game object - D'Arcy
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
        //if (other.tag == "Player")
        //{
        //    other.gameObject.transform.position = GameManager.spawnPoint;
      //  }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            //spawnPoint = kennel.transform.position;
            spawnPoint = player.transform.position; // makes most recent kennel the checkpoint when player collides with the game object - D'Arcy
        }
    }
}
