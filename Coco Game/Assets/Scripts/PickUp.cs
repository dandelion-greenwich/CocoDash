using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int treatValue = 1; // set the treat value - D'Arcy
    private void OnTriggerEnter(Collider other)
    {
        GameManager.AddTreats(treatValue);
        Destroy(gameObject);// when player picks up treat, 'treat score' increases by 1 and treat disappears - D'Arcy
        // 'treats left' goes down
    }
}
