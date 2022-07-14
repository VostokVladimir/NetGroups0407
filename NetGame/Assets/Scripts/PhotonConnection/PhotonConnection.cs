using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhotonConnection
{
    internal static void Connect(string gameVersion)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    internal static void DisConnect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }

    }
}
