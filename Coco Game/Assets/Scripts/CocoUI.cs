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
    }
}
