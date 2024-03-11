using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource footstepsSound;
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public AudioSource pickUpSound; 

    void Update()
    {
        // Check if player is moving (W, A, S, D) and not jumping
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !playerMovement.jumping)
        {
            if (!footstepsSound.isPlaying) 
            {
                footstepsSound.Play(); 
            }
        }
        else
        {
            footstepsSound.Stop(); 
        }
    }

    public void PlayPickUpSound()
    {
        if (!pickUpSound.isPlaying) 
        {
            pickUpSound.Play();
        }
    }
}
