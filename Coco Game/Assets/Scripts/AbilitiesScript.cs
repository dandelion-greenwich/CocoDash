using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesScript : MonoBehaviour
{
    public bool dashState, poopState;
    public float dashSpeed;
    public Transform poop;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        Pooping();
    }

    public void Dash()
    {
        if (Input.GetKey(KeyCode.E) && dashState == true)
        {
            rb.AddForce(transform.forward * dashSpeed + transform.up * dashSpeed / 5, ForceMode.Impulse);
            /*rb.velocity = transform.forward * dashSpeed + transform.up * dashSpeed / 4;*/
            Debug.Log(rb.velocity);
            dashState = false;
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            dashState = true;
        }
    }

    public void Pooping()
    {
        if (Input.GetKey(KeyCode.Alpha2) && poopState == true)
        {
            GameObject.Instantiate(poop, transform.position + transform.forward * -1f, Quaternion.identity);
            poopState = false;
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            poopState = true;
        }
    }
}
