using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CocoUI : MonoBehaviour
{
    public TextMeshProUGUI treatCounter;
    public TextMeshProUGUI lifeCounter;
    public void UpdateTreats()
    {
        treatCounter.text = GameManager.treats.ToString(); //added UI to increase treat counter - D'Arcy

        // treats collected

        //treats left to collect
    }

    public void UpdateLives()
    {
        /*lifeCounter.text = GameManager.lives.ToString();*/
    }

    // Start is called before the first frame update
    private void Start()
    {
        //all three hearts are showing at the start of the game
    }

    // method for unusable mechanics being greyed out

    // three mechanics in bottom left of the screen

    // Update is called once per frame
    void Update()
    {
        
    }
}
