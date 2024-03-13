using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CocoUI : MonoBehaviour
{
    public TextMeshProUGUI treatsCollectedCounter;
    public TextMeshProUGUI treatsLeftCounter;

    // for mechanics ui, would probably need two different images layered on top of each other, so when one is not usuable, the other ui image shows

    public void UpdateTreats()
    {
        treatsCollectedCounter.text = GameManager.treatsCollected.ToString(); //added UI to increase treat counter - D'Arcy
        treatsLeftCounter.text = GameManager.treatsLeft.ToString();
        // treats left to collect, 35 treats overall, set at awake to start 'treats left' at 35, and to go down whenever one is picked up
    }
}
