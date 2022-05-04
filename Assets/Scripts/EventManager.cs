using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public  class EventManager 
{
    public static event Action<int> OnDamageTaken;
    public static event Action<int> OnEnemyKilled;
    public static event Action<int> OnBonusPickUp;
    public static event Action<float> OnChannellingStarted;
    public static event Action OnChannellingStoped;
    public static void SendDamage()
    {
        OnDamageTaken?.Invoke(GameManager.RemoveHealth());
    }
    public static void SendEnemyKilled()
    {
        OnEnemyKilled?.Invoke(GameManager.AddPoint());
    }
    public static void SendBonusPickedUp()
    {
        OnBonusPickUp?.Invoke(GameManager.AddHealth());
    }
    public static void SendChannellingStarted(float _maxTime)
    {
        OnChannellingStarted?.Invoke(_maxTime);
    }
    public static void SendChannellingStoped()
    {
        OnChannellingStoped?.Invoke();

    }
}
