using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesScript : MonoBehaviour
{
    public bool dashState, poopState, barkState;
    public float dashSpeed;
    public Rigidbody poop;
    [SerializeField] Transform buttPosition;
    public GameObject barkSpherePrefab;
    float barkRadius = 5f;
    float barkDuration = 1f;
    public AudioClip barkSound, poopSound, dashSound;

    Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        Pooping();
        Barking();
    }

    public void Dash()
    {
        if (Input.GetKey(KeyCode.Alpha1) && dashState == true)
        {
            rb.AddForce(transform.forward * dashSpeed + transform.up * dashSpeed / 5, ForceMode.Impulse);
            if (dashSound) audioSource.PlayOneShot(dashSound);
            dashState = false;
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            dashState = true;
        }
    }

    public void Pooping()
    {
        if (Input.GetKey(KeyCode.Alpha2) && poopState == true)
        {
            Rigidbody dropPoop = Instantiate(poop, buttPosition.position, buttPosition.rotation);
            dropPoop.velocity = new Vector3(0f, -2f, 0f);
            if (poopSound) audioSource.PlayOneShot(poopSound);
            poopState = false;
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            poopState = true;
        }
    }


    public void Barking()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && barkState) 
        {
            if (barkSound) audioSource.PlayOneShot(barkSound); 

            // Instantiate the bark sphere as a trigger for enemy reaction
            GameObject barkSphere = Instantiate(barkSpherePrefab, transform.position, Quaternion.identity);
            barkSphere.transform.localScale = new Vector3(barkRadius, barkRadius, barkRadius);
            barkSphere.tag = "Barking";

            Destroy(barkSphere, barkDuration); // Ensure the sphere is removed after the effect duration
            barkState = false; 
        }

        if (Input.GetKeyUp(KeyCode.Alpha3)) 
        {
            barkState = true;
        }
    }
}