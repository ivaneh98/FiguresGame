using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text scoreEnd;
    [SerializeField]
    private TMP_Text resultText;

    [SerializeField]
    private TMP_Text scoreEnemyText;
    [SerializeField]
    private TMP_Text nameEnemyText;
    [SerializeField]
    private TMP_Text lifesEnemyText; 
    [SerializeField]
    private TMP_Text messageBoxText;

    [SerializeField]
    private GameObject[] lifes;
    [SerializeField]
    private GameObject PVP_UI; 
    [SerializeField]
    private GameObject messageBox;

    [SerializeField]
    private Button btRespawn;
    [SerializeField]
    private bool isPVP = false;
    [SerializeField]
    private bool isRespawned = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        EventManager.OnDamageTaken += UpdateLifes;
        EventManager.OnBonusPickUp += UpdateLifes;
        EventManager.OnEnemyKilled += ChangeScore;
        EventManager.OnPVPResult += ShowPVPEndWindow;
        EventManager.OnLostConnection += ForceLogout;
        EventManager.OnEnemyScore += SetEnemyData;
        EventManager.OnPlayerDie += ShowEndWindow;
        EventManager.OnRespawnSuccess += RespawnSuccess;
        EventManager.OnRespawnNotEnough += RespawnFailed;

    }

    private void SetEnemyData(string login, int score, int lifes)
    {
        nameEnemyText.text= $"Противник:\n\r{login}";
        scoreEnemyText.text= $"Счет противника:\n\r{score}";
        lifesEnemyText.text = $"Жизни противника:\n\r{lifes}";

    }
    public void Respawn()
    {
        ClientSend.SendCommand($"Respawn|{PlayerPrefs.GetString("login", "")}");
    }
    private void RespawnSuccess()
    {
        PVP_UI.SetActive(false);
        EventManager.SendContinue();
        btRespawn.interactable = false;
    }
    private void RespawnFailed()
    {
        messageBox.SetActive(true);
        messageBoxText.text = "Недостаточно монет";
    }
    public void CloseMessageBox()
    {
        messageBox.SetActive(false);
    }
    private void ForceLogout()
    {
        SceneManager.LoadScene(0);
    }
    private void ShowEndWindow(int score)
    {
        PVP_UI.SetActive(true);

        resultText.text = "Поражение";
        scoreEnd.text = $"Ваш счет: {score}";

    }
    private void ShowPVPEndWindow(string result, int score)
    {
        PVP_UI.SetActive(true);
        switch (result)
        {
            case "win":
                resultText.text = "Победа";
                scoreEnd.text = $"Ваш счет: {score}";
                break;
            case "loss":
                resultText.text = "Поражение";
                scoreEnd.text = $"Противник забрал ваши очки себе";
                break;
            default:
                break;
        }
    }
    private void ChangeScore(int score)
    {
        scoreText.text = score.ToString();
    }
    private void UpdateLifes(int lifesCount)
    {
        foreach(GameObject obj in lifes)
        {
            obj.SetActive(false);
        }
        for(int i = 0; i < lifesCount; i++)
        {
            lifes[i].SetActive(true);
        }
    }
    private void OnDestroy()
    {
        EventManager.OnDamageTaken -= UpdateLifes;
        EventManager.OnPVPResult -= ShowPVPEndWindow;
        EventManager.OnBonusPickUp -= UpdateLifes;
        EventManager.OnEnemyKilled -= ChangeScore;
        EventManager.OnLostConnection -= ForceLogout;
        EventManager.OnEnemyScore -= SetEnemyData;
        EventManager.OnPlayerDie -= ShowEndWindow;
        EventManager.OnRespawnSuccess -= RespawnSuccess;
        EventManager.OnRespawnNotEnough -= RespawnFailed;
    }
    public void Back()
    {
        if (isPVP)
        {
            ClientSend.SendCommand("TechnicalLoss");
        }
        GameManager.instance.Reset();
        SceneManager.LoadScene(0);

    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
