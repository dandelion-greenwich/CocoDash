using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalMov, verticalMov, speedMov, turnSmoothTime = 0.1f, jumpingHeight, gravity, yVel;
    private float turnSmoothVelocity;
    public bool jumping;
    private Animator animator;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        horizontalMov = Input.GetAxisRaw("Horizontal"); //gets horizontal input for player
        verticalMov = Input.GetAxisRaw("Vertical"); //gets vertical input for player

        Vector3 movement = new Vector3(horizontalMov, 0.0f, verticalMov); 

        // Check if the player is moving
        bool isWalking = movement.magnitude > 0.1f; 
        animator.SetBool("isWalking", isWalking); // Update the Animator parameter
    }

    public void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        JumpingFunction(rb);
        //Debug.Log(yVel);

        Vector3 lookAtCam = mainCamera.transform.right * horizontalMov + mainCamera.transform.forward * verticalMov; //makes the player walk towards the camera view
        //rb.velocity = mainCamera.transform.right * horizontalMov + mainCamera.transform.forward * verticalMov; //makes the player walk towards the camera view
        rb.velocity += new Vector3(lookAtCam.x, yVel, lookAtCam.z).normalized; //calculates the movement
        rb.velocity *= speedMov * Time.deltaTime; //adjusts the movement by multiplying by speed and deltaTime to make fps the same

        if (rb.velocity.magnitude > 0.1f) //changes rotation of the character towards where the camera looks only if the character moves
        {
            float targetAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg; /*+ cam.eulerAngles.y;*/ //calucalets where the character model has to look
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); //smoothes the movement
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

    }

    public void JumpingFunction(Rigidbody rb)
    {
        if (Input.GetKey(KeyCode.Space) && jumping == false)
        {
            jumping = true;
            yVel = jumpingHeight;
        }

        if (jumping == true)
        {
            yVel -= gravity * Time.deltaTime;
        }
        else if (jumping == false)
        {
            yVel = 0f;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = false;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = true;
        }
    }
}
