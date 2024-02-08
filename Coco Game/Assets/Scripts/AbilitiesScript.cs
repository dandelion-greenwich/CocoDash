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
        if (Input.GetKey(KeyCode.E) && dash == true)
        {
            rb.AddForce(transform.forward * dashSpeed + transform.up * dashSpeed / 5, ForceMode.Impulse);
            /*rb.velocity = transform.forward * dashSpeed + transform.up * dashSpeed / 4;*/
            Debug.Log(rb.velocity);
            dash = false;
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            dash = true;
        }
    }
}
