using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField loginTextAuth;
    [SerializeField]
    private TMP_InputField passwordTextAuth;
    [SerializeField]
    private TMP_InputField loginTextReg;
    [SerializeField]
    private TMP_InputField passwordTextReg;

    [SerializeField]
    private TMP_Text loginText;
    [SerializeField]
    private TMP_Text messageText; 
    
    [SerializeField]
    private TMP_Text moneyText;

    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text winsText;
    [SerializeField]
    private TMP_Text lossesText;
    [SerializeField]
    private TMP_Text totalGamesText;
    [SerializeField]
    private TMP_Text highscoreText;


    [HideInInspector]
    public string login;
    [HideInInspector]
    public string password;

    public GameObject authUI;
    public GameObject pvpUI;
    public GameObject regUI;
    public GameObject menuUI;
    public GameObject messageBox;
    public GameObject leaderboardUI;
    public GameObject profileUI;

    public Button btStopFindEnemy;
    public Button btGetDailyReward;

    public TMP_Text gameStatus;
    //public InputField usernameField;

    

    private void Awake()
    {
        if (PlayerPrefs.GetInt("id", -1) > 0)
        {
            regUI.SetActive(false);
            authUI.SetActive(false);
            menuUI.SetActive(true);
            loginText.text = PlayerPrefs.GetString("login", login);

            ClientSend.SendCommand($"GetData|{PlayerPrefs.GetString("login", "")}");

        }
        else
        {
            PlayerPrefs.DeleteAll();

        }
    }
    private void Start()
    {
        EventManager.OnLostConnection += ForceLogout;
        EventManager.OnEnemyFound += EnemyFound;
        EventManager.OnFindingEnemy += FindingEnemy;
        EventManager.OnAuthSuccess += AuthSuccess;
        EventManager.OnRegistrationSuccess += RegSuccess;
        EventManager.OnPlayerAuthorizedAlready += PlayerAuthorizedAlready;
        EventManager.OnWrongLogPass += WrongLogPass;
        EventManager.OnUserAlreadyExist += UserAlreadyExist;
        EventManager.OnPlayerDataSuccess += SetPlayerData;
        EventManager.OnLeaderboardSuccess += LeaderboardSuccess;
        EventManager.OnDaityRewardSuccess += DaityRewardSuccess;
    }
    public void OpenLeaderboard()
    {
        ClientSend.SendCommand("GetLeaderboard");
    }
    private void LeaderboardSuccess(Dictionary<string,int> _leaderboard)
    {
        leaderboardUI.SetActive(true);
        Transform leaderboard = GameObject.FindGameObjectWithTag("Scores").transform;
        for (int i = 0; i< leaderboard.childCount;i++)
        {
            leaderboard.GetChild(i).gameObject.SetActive(false);
        }
        int k=0;
        foreach(var leader in _leaderboard)
        {
            Transform obj = leaderboard.GetChild(k);
            obj.gameObject.SetActive(true);
            obj.GetChild(0).GetComponent<TMP_Text>().text = leader.Key;
            obj.GetChild(1).GetComponent<TMP_Text>().text = leader.Value.ToString();
            k++;
        }
    }
    private void SetPlayerData(DateTime lastVisit, int money, int wins, int losses, int highscore)
    {
        PlayerData.lastVisit = lastVisit;
        PlayerData.money = money;
        PlayerData.wins = wins;
        PlayerData.losses = losses;
        PlayerData.highscore = highscore;
        Debug.Log($"{PlayerData.lastVisit};  {PlayerData.money};  {PlayerData.wins};" +
            $"  {PlayerData.losses};  {PlayerData.highscore}");
        moneyText.text = PlayerData.money.ToString();

    }
    public void OpenProfile()
    {
        menuUI.SetActive(false);
        profileUI.SetActive(true);
        nameText.text = PlayerPrefs.GetString("login", login);
        winsText.text = PlayerData.wins.ToString();
        lossesText.text = PlayerData.losses.ToString();
        totalGamesText.text = (PlayerData.wins+ PlayerData.losses).ToString();
        highscoreText.text= PlayerData.highscore.ToString();
        TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
        Debug.Log(DateTime.Now.Subtract(PlayerData.lastVisit));
        Debug.Log(oneDay);
        if (DateTime.Now.Subtract(PlayerData.lastVisit) < oneDay)
            btGetDailyReward.interactable=false;
        else
            btGetDailyReward.interactable = true;

    }
    public void GetDaityReward()
    {
        ClientSend.SendCommand($"GetDaily|{PlayerPrefs.GetString("login", login)}");
    }
    public void DaityRewardSuccess(int money)
    {
        messageBox.SetActive(true);
        PlayerData.money += money;
        messageText.text = $"Ежедневная награда получена!\n\r{money}";
        btGetDailyReward.interactable = false;
        PlayerData.lastVisit= DateTime.Now;
    }
    private void UserAlreadyExist()
    {
        messageBox.SetActive(true);
        messageText.text = "Пользователь с данным логином уже существует.";
    }

    private void PlayerAuthorizedAlready()
    {
        messageBox.SetActive(true);
        messageText.text = "Пользователь с данным логином уже в игре.";
    }
    private void WrongLogPass()
    {
        messageBox.SetActive(true);
        messageText.text = "Логин или пароль не верный.";
    }
    private void ForceLogout()
    {
        SceneManager.LoadScene(0);
    }

    public void HideMessageBox()
    {
        messageBox.SetActive(false);
    }
    public void Auth()
    {
        PlayerPrefs.SetString("state", "auth");

        login = loginTextAuth.text;
        password = passwordTextAuth.text;
        PlayerPrefs.SetString("login", login);
        PlayerPrefs.SetString("password", password);
        if (login != "" && login.Length <= 8)
        {
            if (password != "")
            {
                Client.instance.ConnectToServer();
            }
            else
            {
                messageBox.SetActive(true);
                messageText.text = "Введите пароль.";
            }
        }
        else
        {
            messageBox.SetActive(true);
            messageText.text = "Размер логина должен быть от 1 до 8 символов.";
        }

    }
    public void Reg()
    {
        PlayerPrefs.SetString("state", "reg");

        login = loginTextReg.text;
        password = passwordTextReg.text;
        PlayerPrefs.SetString("login", login);
        PlayerPrefs.SetString("password", password);
        if (login != "" && login.Length <= 8)
        {
            if (password != "")
            {
                Client.instance.ConnectToServer();
            }
            else
            {
                messageBox.SetActive(true);
                messageText.text = "Введите пароль.";
            }
        }
        else
        {
            messageBox.SetActive(true);
            messageText.text = "Размер логина должен быть от 1 до 8 символов.";
        }

    }
    public void AuthSuccess()
    {
        authUI.SetActive(false);
        menuUI.SetActive(true);
        PlayerPrefs.SetInt("id", Client.instance.myId);
        PlayerPrefs.SetString("login", login);
        loginText.text = login;
        ClientSend.SendCommand($"GetData|{PlayerPrefs.GetString("login", "")}");
    }
    public void RegSuccess()
    {
        authUI.SetActive(true);
        regUI.SetActive(false);
    }
    public void AuthOpen()
    {
        authUI.SetActive(true);
        regUI.SetActive(false);
    }
    public void RegOpen()
    {
        authUI.SetActive(false);
        regUI.SetActive(true);
    }
    public void PVPOpen()
    {
        menuUI.SetActive(false);
        pvpUI.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void FindEnemy()
    {
        ClientSend.SendCommand($"FindEnemy|{login}");
    }
    public void MenuOpen()
    {
        profileUI.SetActive(false);
        leaderboardUI.SetActive(false);
        pvpUI.SetActive(false);
        menuUI.SetActive(true);
        moneyText.text = PlayerData.money.ToString();

    }
    public void EnemyFound()
    {
        SceneManager.LoadScene(2);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void FindingEnemy()
    {
        gameStatus.text = "Поиск противника...";
        btStopFindEnemy.interactable = false;
    }
    public void StopFindingEnemy()
    {
        btStopFindEnemy.interactable = true;
        gameStatus.text = "";
        ClientSend.SendCommand("CloseRoom");
        MenuOpen();
    }
    private void OnDestroy()
    {
        EventManager.OnLostConnection -= ForceLogout;
        EventManager.OnEnemyFound -= EnemyFound;
        EventManager.OnFindingEnemy -= FindingEnemy;
        EventManager.OnAuthSuccess -= AuthSuccess;
        EventManager.OnRegistrationSuccess -= RegSuccess;
        EventManager.OnPlayerAuthorizedAlready -= PlayerAuthorizedAlready;
        EventManager.OnWrongLogPass -= WrongLogPass;
        EventManager.OnUserAlreadyExist -= UserAlreadyExist;
        EventManager.OnPlayerDataSuccess -= SetPlayerData;
        EventManager.OnLeaderboardSuccess -= LeaderboardSuccess;
    }
}
