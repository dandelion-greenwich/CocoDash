using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool pickedUp = false;
    public int treatValue = 1; // set the treat value - D'Arcy
    private void OnTriggerEnter(Collider other)
    {
            pickedUp = true;
            GameManager.AddTreats(treatValue); // when player picks up treat, 'treat score' increases by 1 - D'Arcy
    }

    private void OnTriggerExit(Collider other)
    {
        pickedUp = false; //added basic pickup system - D'Arcy
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            Destroy(gameObject); //destroys 'treat' once player has walked over it - D'Arcy
        }
    }
}
