using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    private bool isPlay = true;
    private void Start()
    {
        EventManager.OnPVPResult += StopCheck;
        EventManager.OnPlayerDie += StopCheck;
        EventManager.OnContinue += Continue;
    }

    private void StopCheck(string arg1, int arg2)
    {
        Destroy(gameObject);
    }
    private void StopCheck(int arg2)
    {
        isPlay = false;
    }
    private void Continue()
    {
        isPlay = true;
    }
    public string objTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlay)
        {
            if (collision.CompareTag(objTag))
            {
                EventManager.SendEnemyKilled();
                Destroy(collision.gameObject);
            }
            else if (!collision.CompareTag("Player"))
            {
                EventManager.SendDamage();
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }

    }
    private void OnDestroy()
    {
        EventManager.OnPVPResult -= StopCheck;
        EventManager.OnPlayerDie -= StopCheck;
        EventManager.OnContinue -= Continue;

    }
}
