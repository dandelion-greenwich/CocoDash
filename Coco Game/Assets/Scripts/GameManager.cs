using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int treatsCollected;
    public static int treatsLeft;
    static CocoUI cocoUI;
    public static Vector3 spawnPoint;

    // Start is called before the first frame update
    private void Awake()
    {
        cocoUI = FindObjectOfType<CocoUI>();
        treatsCollected = 0; // player begins with no treats
        treatsLeft = 35; // all treats left to collect at the start of the game - D'Arcy
        cocoUI.UpdateTreats(); // treats UI
    }

    public static void AddTreats(int TreatValue)
    {
        treatsCollected += TreatValue; // add 1 treat to score
        treatsLeft -= TreatValue; // minus 1 treat from treats left to collect
        cocoUI.UpdateTreats(); // added code to update treat counter - D'Arcy
    }
}
