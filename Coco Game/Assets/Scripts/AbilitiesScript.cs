using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesScript : MonoBehaviour
{
    public bool dashState, poopState, barkState, abilityActive, allTreatsCollected;
    public float dashSpeed, barkRadius = 5f, barkDuration = 1f;
    public int dashingValue, poopingValue, barkingValue;
    public Rigidbody poop;
    [SerializeField] Transform buttPosition;
    public GameObject barkSpherePrefab;
    public GameObject canvas;
    CocoUI cocoUI;
    GameManager gameManager;
    public AudioClip barkSound, poopSound, dashSound;

    Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        gameManager = gameObject.AddComponent<GameManager>();
        cocoUI = canvas.GetComponent<CocoUI>();
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashState && abilityActive && cocoUI.currentState == CocoUI.GameState.Active)
        {
            StartCoroutine(LimitationDelay(3f));
            abilityActive = false;
            rb.AddForce(transform.forward * dashSpeed + transform.up * dashSpeed / 5, ForceMode.Impulse);
            if (dashSound) audioSource.PlayOneShot(dashSound);
            dashState = false;
            GameManager.treatsCollected -= dashingValue;
        }
    }

    public void Pooping()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && poopState && abilityActive && cocoUI.currentState == CocoUI.GameState.Active)
        {
            StartCoroutine(LimitationDelay(3f));
            abilityActive = false;
            Rigidbody dropPoop = Instantiate(poop, buttPosition.position, new Quaternion(0f, 0f, 0f, 0f));
            dropPoop.velocity = new Vector3(0f, -2f, 0f);
            if (poopSound) audioSource.PlayOneShot(poopSound);
            poopState = false;
            GameManager.treatsCollected -= poopingValue;
        }
    }


    public void Barking()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && barkState && abilityActive && cocoUI.currentState == CocoUI.GameState.Active) 
        {
            abilityActive = false;
            StartCoroutine(LimitationDelay(3f));
            if (barkSound) audioSource.PlayOneShot(barkSound); 

            // Instantiate the bark sphere as a trigger for enemy reaction
            GameObject barkSphere = Instantiate(barkSpherePrefab, transform.position, Quaternion.identity);
            barkSphere.transform.localScale = new Vector3(barkRadius, barkRadius, barkRadius);
            barkSphere.tag = "Barking";

            Destroy(barkSphere, barkDuration); // Ensure the sphere is removed after the effect duration
            barkState = false; // Prevent spamming the bark ability
            GameManager.treatsCollected -= barkingValue;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3)) 
        {
            barkState = true;
        }
    }
    public void CheckValue()
    {
        if (GameManager.treatsCollected >= dashingValue)
        {
            dashState = true;
        }
        else
        {
            dashState = false;
        }
        if (GameManager.treatsCollected >= poopingValue)
        {
            poopState = true;
        }
        else
        {
            poopState = false;
        }
        if (GameManager.treatsCollected >= barkingValue)
        {
            barkState = true;
        }
        else
        {
            barkState = false;
        }
    }

    IEnumerator LimitationDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        abilityActive = true;
    }
    /*    IEnumerator ResetFleeingState(float delay)
        {
            yield return new WaitForSeconds(delay);
            isFleeing = false;
        }*/
/*    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint" && allTreatsCollected && other.gameObject != null)
        {
            cocoUI.CheckGameState(CocoUI.GameState.Victory);
        }
    }*/
}