using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CocoUI : MonoBehaviour
{
    public TextMeshProUGUI treatCounter;

    // for mechanics ui, would probably need two different images layered on top of each other, so when one is not usuable, the other ui image shows

    public void UpdateTreats()
    {
        treatCounter.text = GameManager.treats.ToString(); //added UI to increase treat counter - D'Arcy

        // treats collected

        // treats left to collect
    }
}
