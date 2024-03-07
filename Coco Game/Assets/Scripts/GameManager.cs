using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int treats;
    static CocoUI cocoUI;
    public static Vector3 spawnPoint;

    // Start is called before the first frame update
    private void Awake()
    {
        cocoUI = FindObjectOfType<CocoUI>();
        treats = 0;
        cocoUI.UpdateTreats(); // treats UI
    }

    public static void AddTreats(int TreatValue)
    {
        treats += TreatValue;
        cocoUI.UpdateTreats(); // added code to update treat counter - D'Arcy
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
