using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesScript : MonoBehaviour
{
    public bool dashState, poopState, barkState, abilityActive, allTreatsCollected;
    public float dashSpeed, barkRadius = 5f, barkDuration = 1f;
    public int dashingValue, poopingValue, barkingValue;
    public Rigidbody poop;
    [SerializeField] Transform buttPosition;
    public GameObject barkSpherePrefab;
    GameManager gameManager;
    CocoUI cocoUI;
    public AudioClip barkSound, poopSound, dashSound;

    Rigidbody rb;
    private AudioSource audioSource;

    private GameObject mech1usable;
    private GameObject mech1unusable;
    private GameObject mech2usable;
    private GameObject mech2unusable;
    private GameObject mech3usable;
    private GameObject mech3unusable;
    /*int mechCount;*/
    /*public bool mech1usable, mech1unusable, mech2usable, mech2unusable, mech3usable, mech3unusable;*/
    /*    public int mechanics = 3;
        public Image[] mechs;
        public Sprite mechUsable;
        public Sprite mechUnusable;*/

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        gameManager = gameObject.AddComponent<GameManager>();
        cocoUI = GetComponent<CocoUI>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckValue();
        Dash();
        Pooping();
        Barking();

/*        foreach (Image img in mechs)
        {
            img.sprite = mechUnusable;
        }
        for (int i = 0; i < mechanics; i++)
        {
            mechs[i].sprite = mechUsable;
        }*/
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashState && abilityActive)
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && poopState && abilityActive)
        {
            StartCoroutine(LimitationDelay(3f));
            abilityActive = false;
            Rigidbody dropPoop = Instantiate(poop, buttPosition.position, buttPosition.rotation);
            dropPoop.velocity = new Vector3(0f, -2f, 0f);
            if (poopSound) audioSource.PlayOneShot(poopSound);
            poopState = false;
            GameManager.treatsCollected -= poopingValue;
        }
    }


    public void Barking()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && barkState && abilityActive) 
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
            /*mech1usable = true;*/
            // mech1usable shows
        }
        else
        {
            dashState = false;
            /*mech1unusable = true;*/
            // mech1unusable shows
        }
        if (GameManager.treatsCollected >= poopingValue)
        {
            poopState = true;
            /*mech2usable = true;*/
            // mech2usable shows
        }
        else
        {
            poopState = false;
            /*mech2unusable = true;*/
            // mech2unusable shows
        }
        if (GameManager.treatsCollected >= barkingValue)
        {
            barkState = true;
            /*mech3usable = true;*/
            // mech3usable shows
        }
        else
        {
            barkState = false;
            /*mech3unusable = true;*/
            // mech3unusable shows
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

    private void Awake()
    {
        mech1usable = GameObject.Find("Mech1Usable");
        mech1unusable = GameObject.Find("Mech1Unusable");
        mech2usable = GameObject.Find("Mech2Usable");
        mech2unusable = GameObject.Find("Mech2Unusable");
        mech3usable = GameObject.Find("Mech3Usable");
        mech3unusable = GameObject.Find("Mech3Unusable"); // finds these game objects when the ui wakes up - D'Arcy
    }
}