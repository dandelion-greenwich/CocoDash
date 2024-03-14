using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CocoUI : MonoBehaviour
{
    public float countdown;
    int minutes, seconds;
    public TextMeshProUGUI treatsCollectedCounter;
    public TextMeshProUGUI treatsLeftCounter, timerCountdown;

    // for mechanics ui, need two different images layered on top of each other, so when one is not usuable, the other ui image shows

    public void UpdateTreats()
    {
        treatsCollectedCounter.text = GameManager.treatsCollected.ToString(); //added UI to increase treat counter - D'Arcy
        treatsLeftCounter.text = GameManager.treatsLeft.ToString();
    }
    private void Update()
    {
        treatsCollectedCounter.text = GameManager.treatsCollected.ToString(); //added UI to increase treat counter - D'Arcy
        treatsLeftCounter.text = GameManager.treatsLeft.ToString();
        Timer();
    }
    public void Timer()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else if (countdown <= 0)
        {
            countdown = 0;
        }
        minutes = Mathf.FloorToInt(countdown / 60);
        seconds = Mathf.FloorToInt(countdown % 60);
        timerCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds); //Has a bug when after reaching 00:00 for a split second it shows 1
    }
}
