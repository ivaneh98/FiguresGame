using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write($"{PlayerPrefs.GetString("state", "")}|{PlayerPrefs.GetString("login", "")}|{PlayerPrefs.GetString("password", "")}");
            Debug.Log($"{PlayerPrefs.GetString("state", "")}|{PlayerPrefs.GetString("login", "")}|{PlayerPrefs.GetString("password", "")}");

            SendTCPData(_packet);
        }
    }
    public static void SendCommand(string _command)
    {
        using (Packet _packet = new Packet((int)ClientPackets.commandReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(_command);

            SendTCPData(_packet);
        }
    }
    #endregion
}
