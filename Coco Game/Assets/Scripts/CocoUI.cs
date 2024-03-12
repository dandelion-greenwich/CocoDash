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
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        treatCounter.text = GameManager.treats.ToString(); //added the line so it would check treats count every second - Serhii
    }
}
