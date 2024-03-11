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
            if (!footstepsSound.isPlaying) // Check if the sound is not already playing
            {
                footstepsSound.Play(); // Play sound if not already playing
            }
        }
        else
        {
            footstepsSound.Stop(); // Stop the sound if conditions are not met
        }
    }

    public void PlayPickUpSound()
    {
        if (!pickUpSound.isPlaying) // Play the sound only if it's not already playing
        {
            pickUpSound.Play();
        }
    }
}
