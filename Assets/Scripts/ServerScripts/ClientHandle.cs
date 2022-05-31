using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();
    }
    public static void Command(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        string[] _command = _msg.Split('|');
        Debug.Log(_msg);
        switch (_command[0])
        {
            case "AuthSuccess":
                EventManager.SendAuthSuccess();
                break;
            case "RegistrationSuccess":
                EventManager.SendRegSuccess();
                break;
            case "EnemyFound":
                EventManager.SendEnemyFound();
                break;
            case "FindingEnemy":
                EventManager.SendFindingEnemy();
                break;
            case "DelayedSpawn":
                EventManager.SendDelayedSpawn(int.Parse(_command[1]), float.Parse(_command[2]));
                break;
            case "MatchResult":
                EventManager.SendPVPResult(_command[1], int.Parse(_command[2]));
                break;
            case "PlayerAuthorizedAlready":
                EventManager.SendPlayerAuthorizedAlready();
                break;
            case "WrongLogPass":
                EventManager.SendWrongLogPass();
                break;
            case "DataSuccess"://получить данные пользователя
                EventManager.SendPlayerData(Utilites.ConvertToDateTime(_command[1]), int.Parse(_command[2]),
                    int.Parse(_command[3]), int.Parse(_command[4]), int.Parse(_command[5]));
                break;
            case "EnemyScore"://получить логин и счет противника
                EventManager.SendEnemyScore(_command[1], int.Parse(_command[2]), int.Parse(_command[3]));
                break;
            case "UserAlreadyExist":
                EventManager.SendUserAlreadyExist();
                break;
            case "LeaderboardSuccess":
                string[] leaderscore = _command[1].Split('~');
                Dictionary<string, int> leaderboard = new Dictionary<string, int>();
                for (int i = 0; i < leaderscore.Length; i=i+2)
                {
                    leaderboard.Add(leaderscore[i], int.Parse(leaderscore[i + 1]));
                }
                EventManager.SendLeaderboardSuccess(leaderboard);
                break;
            case "DaityRewardSuccess":
                EventManager.SendDaityRewardSuccess(int.Parse(_command[1]));
                break;
            case "RespawnSuccess":
                EventManager.SendRespawnSuccess();
                break;
            case "RespawnNotEnough":
                EventManager.SendRespawnNotEnough();
                break;
        }

    }
}
