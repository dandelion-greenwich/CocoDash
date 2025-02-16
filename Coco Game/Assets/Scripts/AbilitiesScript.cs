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
    public GameObject canvas;
    CocoUI cocoUI;
    GameManager gameManager;
    public AudioClip barkSound, poopSound, dashSound;

    Rigidbody rb;
    private AudioSource audioSource;

    public Image dashUI;
    public float coolDown1 = 3f;
    public bool isCoolDown1 = false;

    public Image poopUI;
    public float coolDown2 = 3f;
    public bool isCoolDown2 = false;

    public Image barkUI;
    public float coolDown3 = 3f;
    public bool isCoolDown3 = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        gameManager = gameObject.AddComponent<GameManager>();
        cocoUI = canvas.GetComponent<CocoUI>();

        dashUI.fillAmount = 1;
        poopUI.fillAmount = 1;
        barkUI.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckValue();
        Dash();
        Pooping();
        Barking();

        
        // dash ability
        if(isCoolDown1 == true)
        {
            dashUI.fillAmount += 1 / coolDown1 * Time.deltaTime; // shows a cooldown 'timer' on the dash mechanic UI - D'Arcy
        }
        if(dashUI.fillAmount == 1)
        {
            isCoolDown1 = false;
        }

        // poop ability
        if (isCoolDown2 == true)
        {
            poopUI.fillAmount += 1 / coolDown1 * Time.deltaTime; // shows a cooldown 'timer' on the poop mechanic UI - D'Arcy
        }
        if (poopUI.fillAmount == 1)
        {
            isCoolDown2 = false;
        }

        // bark ability
        if (isCoolDown3 == true)
        {
            barkUI.fillAmount += 1 / coolDown1 * Time.deltaTime; // shows a cooldown 'timer' on the bark mechanic UI - D'Arcy
        }
        if (barkUI.fillAmount == 1)
        {
            isCoolDown3 = false;
        }
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
            if(isCoolDown1 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown1 = true;
                dashUI.fillAmount = 0;
            }
            if (isCoolDown2 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown2 = true;
                poopUI.fillAmount = 0;
            }
            if (isCoolDown3 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown3 = true;
                barkUI.fillAmount = 0;
            } // cooldown visual for all three mechanics when one is used - D'Arcy
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
            if (isCoolDown2 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown2 = true;
                poopUI.fillAmount = 0;
            }
            if (isCoolDown1 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown1 = true;
                dashUI.fillAmount = 0;
            }
            if (isCoolDown3 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown3 = true;
                barkUI.fillAmount = 0;
            } // cooldown visual for all three mechanics when one is used - D'Arcy
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
            if (isCoolDown3 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown3 = true;
                barkUI.fillAmount = 0;
            }
            if (isCoolDown2 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown2 = true;
                poopUI.fillAmount = 0;
            }
            if (isCoolDown1 == true)
            {
                Debug.Log("cooldown");
            }
            else
            {
                isCoolDown1 = true;
                dashUI.fillAmount = 0;
            } // cooldown visual for all three mechanics when one is used - D'Arcy
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
}