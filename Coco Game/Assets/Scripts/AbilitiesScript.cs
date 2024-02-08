using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesScript : MonoBehaviour
{
    public bool dash = true;
    public float dashSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }

    public void Dash()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        }
    }
}
