using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    /*public static GameManager instance;*/
    public static int treatsCollected, treatsLeft, health = 3;
    static CocoUI cocoUI;
    static AbilitiesScript abilitiesScript;
    public static Vector3 spawnPoint;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        cocoUI = FindObjectOfType<CocoUI>();
        abilitiesScript = GetComponent<AbilitiesScript>();
        treatsCollected = 0; // player begins with no treats
        treatsLeft = 5; // all treats left to collect at the start of the game - D'Arcy
        cocoUI.UpdateTreats(); // treats UI
    }
    private void Update()
    {
        GameOver();
        //Debug.Log(health);
    }

    public static void AddTreats(int TreatValue)
    {
        treatsCollected += TreatValue; // adds 1 treat to score
        treatsLeft -= TreatValue; // subtracts 1 treat from treats left to collect
        cocoUI.UpdateTreats(); // added code to update treat counter - D'Arcy
        if (treatsLeft <= 0)
        {
            abilitiesScript.allTreatsCollected = true;
            treatsLeft = 0;
        }
    }
    public static void GameOver()
    {
        if (health <= 0 || cocoUI.countdown <= 0)
        {
            cocoUI.CheckGameState(CocoUI.GameState.Loss);
        }
    }
}
