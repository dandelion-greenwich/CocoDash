using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int treats = 0, goalTreats = 20, health = 3;
    static CocoUI cocoUI;
    public static Vector3 spawnPoint;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        cocoUI = FindObjectOfType<CocoUI>();
        cocoUI.UpdateTreats(); // treats UI
    }

    public static void AddTreats(int TreatValue)
    {
        treats += TreatValue;
        goalTreats -= TreatValue;
        cocoUI.UpdateTreats(); // added code to update treat counter - D'Arcy
    }
    public void GoalTreats()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
    }
}
