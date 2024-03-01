using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesScript : MonoBehaviour
{
    public bool dashState, poopState;
    public float dashSpeed;
    public Rigidbody poop;
    [SerializeField] Transform buttPosition;

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
        if (Input.GetKey(KeyCode.Alpha1) && dashState == true)
        {
            rb.AddForce(transform.forward * dashSpeed + transform.up * dashSpeed / 5, ForceMode.Impulse);
            dashState = false;
        }
        if(Input.GetKeyUp(KeyCode.Alpha1))
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
            poopState = false;
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            poopState = true;
        }
    }
}
