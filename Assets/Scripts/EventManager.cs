using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public  class EventManager 
{
    public static event Action OnRespawnNotEnough;
    public static event Action OnRespawnSuccess;
    public static event Action<int> OnDamageTaken;
    public static event Action<int> OnEnemyKilled;
    public static event Action<int> OnPlayerDie;
    public static event Action<int> OnBonusPickUp;
    public static event Action<int> OnDaityRewardSuccess;
    public static event Action<float> OnChannellingStarted;
    public static event Action OnChannellingStoped;
    public static event Action OnEnemyFound;
    public static event Action OnContinue;
    public static event Action OnFindingEnemy;
    public static event Action OnAuthSuccess;
    public static event Action OnLostConnection; 
    public static event Action OnPlayerAuthorizedAlready;
    public static event Action OnWrongLogPass;
    public static event Action OnUserAlreadyExist;
    public static event Action OnRegistrationSuccess;
    public static event Action<int,float> OnDelayedSpawn;
    public static event Action<Dictionary<string, int>> OnLeaderboardSuccess;
    public static event Action<string, int> OnPVPResult;
    public static event Action<string, int, int> OnEnemyScore;
    public static event Action<DateTime, int, int, int, int> OnPlayerDataSuccess;

    public static void SendDamage()
    {
        OnDamageTaken?.Invoke(GameManager.instance.RemoveHealth());
    }
    public static void SendEnemyKilled()
    {
        OnEnemyKilled?.Invoke(GameManager.instance.AddPoint());
    }
    public static void SendBonusPickedUp()
    {
        OnBonusPickUp?.Invoke(GameManager.instance.AddHealth());
    }
    public static void SendChannellingStarted(float _maxTime)
    {
        OnChannellingStarted?.Invoke(_maxTime);
    }
    public static void SendChannellingStoped()
    {
        OnChannellingStoped?.Invoke();
    }
    public static void SendEnemyFound()
    {
        OnEnemyFound?.Invoke();
    }
    public static void SendFindingEnemy()
    {
        OnFindingEnemy?.Invoke();
    }
    public static void SendRegSuccess()
    {
        OnRegistrationSuccess?.Invoke();
    }
    public static void SendAuthSuccess()
    {
        OnAuthSuccess?.Invoke();
    }
    public static void SendLostConnection()
    {
        OnLostConnection?.Invoke();
    }
    public static void SendDelayedSpawn(int id, float position)
    {
        OnDelayedSpawn?.Invoke(id,position);
    }
    public static void SendPlayerDead(int score)
    {
        OnPlayerDie?.Invoke(score);
    }
    public static void SendPVPResult(string result,int score)
    {
        OnPVPResult?.Invoke(result,score);
    }
    public static void SendPlayerAuthorizedAlready()
    {
        OnPlayerAuthorizedAlready?.Invoke();
    }
    public static void SendWrongLogPass()
    {
        OnWrongLogPass?.Invoke();
    }
    public static void SendUserAlreadyExist()
    {
        OnUserAlreadyExist?.Invoke();
    }
    public static void SendPlayerData(DateTime lastVisit, int money, int wins, int losses, int highscore)
    {
        OnPlayerDataSuccess?.Invoke(lastVisit, money, wins, losses, highscore);
    }
    public static void SendEnemyScore(string login, int score, int lifes)
    {
        OnEnemyScore?.Invoke(login, score, lifes);
    }
    public static void SendLeaderboardSuccess(Dictionary<string, int> leaderboard)
    {
        OnLeaderboardSuccess?.Invoke(leaderboard);
    }
    public static void SendDaityRewardSuccess(int money)
    {
        OnDaityRewardSuccess?.Invoke(money);
    }
    public static void SendRespawnSuccess()
    {
        OnRespawnSuccess?.Invoke();
    }
    public static void SendRespawnNotEnough()
    {
        OnRespawnNotEnough?.Invoke();
    }
    public static void SendContinue()
    {
        OnContinue?.Invoke();
    }
}
