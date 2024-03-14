using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int treatValue = 1; // set the treat value - D'Arcy

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider is tagged as "Player"
        {
            GameManager.AddTreats(treatValue);
            FindObjectOfType<AudioManager>().PlayPickUpSound(); // Call the method on AudioManager to play the sound
            Destroy(gameObject); // When player picks up treat, 'treat score' increases by 1 - D'Arcy
        }
    }
}
