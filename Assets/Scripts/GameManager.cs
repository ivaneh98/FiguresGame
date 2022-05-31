using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isPVP = false;
    public static GameManager instance;
    private static int score = 0;
    private static int scoreSended = 0;
    [SerializeField]
    private static int health = 5;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    private void Start()
    {
        if (isPVP)
            ClientSend.SendCommand("PlayerReady");
        else
        {
            EventManager.OnContinue += Respawn;
        }
    }

    private void Respawn()
    {
        health = 4;
        EventManager.SendBonusPickedUp();
    }

    public void Reset()
    {
        score = 0;
        health = 5;
    }


    public int AddPoint()
    {
        if (isPVP)
            ClientSend.SendCommand("AddPoint");
        score++;
        return score;
    }
    public int AddHealth()
    {
        if (isPVP)
            ClientSend.SendCommand("AddHealth");
        health++; 
        return health;
    }
    public int RemoveHealth()
    {
        health--;
        if (isPVP)
            ClientSend.SendCommand("RemoveHealth");
        else
        {
            if (health <= 0)
            {
                EventManager.SendPlayerDead(score);
                if (score > PlayerData.highscore)
                {
                    PlayerData.highscore = score;
                    ClientSend.SendCommand($"SetHighscore|" +
                        $"{PlayerPrefs.GetString("login", "")}|{PlayerData.highscore}");
                    int sendScore = score - scoreSended;
                    ClientSend.SendCommand($"AddMoney|" +
                        $"{PlayerPrefs.GetString("login", "")}|{sendScore}");
                    scoreSended = score;
                }
            }

        }
        return health;
    }
    private void OnDestroy()
    {
        EventManager.OnContinue -= Respawn;
    }

}
