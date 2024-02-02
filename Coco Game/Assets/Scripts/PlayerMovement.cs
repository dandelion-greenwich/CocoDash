using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalMov, verticalMov, speedMov, turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        horizontalMov = Input.GetAxisRaw("Horizontal"); //gets horizontal input for 
        verticalMov = Input.GetAxisRaw("Vertical"); //gets vertical input for 
    }

    public void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = mainCamera.transform.right * horizontalMov + mainCamera.transform.forward * verticalMov; //makes the player walk towards the camera view
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z).normalized; //calculates the movement
        rb.velocity *= speedMov * Time.deltaTime; //adjusts the movement by multiplying by speed and deltaTime to make fps the same

        if (rb.velocity.magnitude > 0f) //
        {
            float targetAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg; /*+ cam.eulerAngles.y;*/ //calucalets where the character model has to look
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); //smoothes the movement
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
