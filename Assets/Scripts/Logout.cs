using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logout : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {

    }
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Logout");
        if (objs.Length>1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        EventManager.OnAuthSuccess += StartDontLogoutMe;
        EventManager.OnLostConnection += ForceLogout;
    }
    private void ForceLogout()
    {
        Client.instance.myId = -2;
        PlayerPrefs.SetInt("id", -2);
        PlayerPrefs.SetString("login", "");

        isAuth = false;
    }
    bool isAuth = false;
    private void FixedUpdate()
    {
        if (isAuth)
            ClientSend.SendCommand("DontLogoutMe");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("id", -1);
        PlayerPrefs.SetString("login", "");

    }
    private void StartDontLogoutMe()
    {
        isAuth = true;
    }

    private void OnDestroy()
    {
        EventManager.OnAuthSuccess -= StartDontLogoutMe;
        EventManager.OnLostConnection -= ForceLogout;
    }
}
