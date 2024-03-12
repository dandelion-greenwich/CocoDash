using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class AbilitiesScript : MonoBehaviour
{
    public bool dashState, poopState, barkState;
    public float dashSpeed, barkRadius = 5f, barkDuration = 1f;
    public int dashingValue, poopingValue, barkingValue;
    public Rigidbody poop;
    [SerializeField] Transform buttPosition;
    public GameObject barkSpherePrefab;
    GameManager gameManager;
    public AudioClip barkSound, poopSound, dashSound;

    Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        gameManager = gameObject.AddComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckValue();
        Dash();
        Pooping();
        Barking();
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && dashState)
        {
            rb.AddForce(transform.forward * dashSpeed + transform.up * dashSpeed / 5, ForceMode.Impulse);
            if (dashSound) audioSource.PlayOneShot(dashSound);
            dashState = false;
            GameManager.treats -= dashingValue;
        }
    }

    public void Pooping()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && poopState)
        {
            Rigidbody dropPoop = Instantiate(poop, buttPosition.position, buttPosition.rotation);
            dropPoop.velocity = new Vector3(0f, -2f, 0f);
            if (poopSound) audioSource.PlayOneShot(poopSound);
            poopState = false;
            GameManager.treats -= poopingValue;
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
            barkState = false; // Prevent spamming the bark ability
            GameManager.treats -= barkingValue;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3)) 
        {
            barkState = true;
        }
    }
    public void CheckValue()
    {
        if (GameManager.treats >= dashingValue)
        {
            dashState = true;
        }
        else
        {
            dashState = false;
        }
        if (GameManager.treats >= poopingValue)
        {
            poopState = true;
        }
        else
        {
            poopState = false;
        }
        if (GameManager.treats >= barkingValue)
        {
            barkState = true;
        }
        else
        {
            barkState = false;
        }
    }
}