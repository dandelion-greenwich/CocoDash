using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int treats;
    public static int lives;
    static CocoUI cocoUI;
    public static Vector3 spawnPoint;

    // Start is called before the first frame update
    private void Awake()
    {
        cocoUI = FindObjectOfType<CocoUI>();
        treats = 0; // player begins with no treats
        lives = 3; // player begins with three lives
        cocoUI.UpdateTreats(); // treats UI
        cocoUI.UpdateLives(); // lives UI
    }

    public static void AddTreats(int TreatValue)
    {
        treats += TreatValue;
        cocoUI.UpdateTreats(); // added code to update treat counter - D'Arcy
    }

    public static void RemoveLives(int LifeValue)
    {
        /*lives -= LifeValue;
        cocoUI.UpdateLives(); // update life counter - D'Arcy*/

        // if player respawns then destroy one heart, hearts must be destroyed in order from left to right
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
