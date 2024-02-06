using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool pickedUp = false;
    void OnTriggerEnter(Collider other)
    {
        pickedUp = true;
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
