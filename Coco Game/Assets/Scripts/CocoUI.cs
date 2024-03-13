using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CocoUI : MonoBehaviour
{
    public TextMeshProUGUI treatCounter;


    public void UpdateTreats()
    {
        treatCounter.text = GameManager.treats.ToString(); //added UI to increase treat counter - D'Arcy

        // treats collected

        // treats left to collect
    }

    public void UpdateLives()
    {
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        //all three hearts are showing at the start of the game
    }

    // method for unusable mechanics being greyed out

    // Update is called once per frame
    void Update()
    {
        
    }
}
