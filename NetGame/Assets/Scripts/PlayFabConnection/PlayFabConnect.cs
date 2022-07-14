using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabConnect : MonoBehaviour,IConnectionCallbacks,IInRoomCallbacks
    {

    [SerializeField]private GameObject _canvac;
    private const string PlayerFab_ID = "5E23E";
    private string _game_version = "Dev";
    private GameObject text;
    private Text textLabel;


    void Awake ()
    {
        PhotonNetwork.AddCallbackTarget(this);
        GameObject panel = _canvac.transform.GetChild(0).gameObject;
        //TODO:"переделать стр 25, так как риск сбоя программы при добавлении обьектов в иерархию Panel"
        text = panel.transform.GetChild(3).gameObject;
        textLabel = text.GetComponent<Text>();
    }

   
    public void PlayFabConnectOnClick()
    {
      var titleId = PlayFabSettings.staticSettings.TitleId;

        if (string.IsNullOrEmpty(titleId))
            titleId = PlayerFab_ID;

        //var request = new LoginWithCustomIDRequest {CustomId="User00" , CreateAccount=true};
        //PlayFabClientAPI.LoginWithCustomID(request, Success, Fail);
    }

    private void Success(LoginResult result)
        {
         Debug.Log($"ProFile Server connection has done {result.PlayFabId}");
       textLabel.color = Color.green;
        textLabel.text = $"ProFile Server connection has done { result.PlayFabId }.Press button Connect Game Server, Please ";
         
        }

    private void Fail(PlayFabError error)
    {
        var errorMesage = error.GenerateErrorReport();
        Debug.LogError($"ProFile Server connection is failed: {errorMesage}");
        textLabel.text = $"ProFile Server connection is failed: {errorMesage}";
    }

    public void ConnectGameServerOnClick()
    {
       PhotonConnection.Connect(_game_version);
        textLabel.color = Color.green;
        textLabel.text = $"Game Server link connected";
    }

    public void DisconnectGameServerOnClick()
    {
        Debug.Log($"Game Server link disconnected");
        textLabel.color = Color.red;
        textLabel.text = $"Game Server link disconnected";
        PhotonConnection.DisConnect();
        
    }

    public void OnConnected()
    {
        Debug.Log($"OnConnected");
        textLabel.color = Color.green;
        textLabel.text = $"OnConnected";
    }

    public void OnConnectedToMaster()
    {
        textLabel.color = Color.green;
        Debug.Log($"OnConnectedToMaster");
        textLabel.text = $"OnConnectedToMaster";
        PhotonNetwork.JoinRandomOrCreateRoom();
        Debug.Log($"Room is ready for Master");
        textLabel.text = $"Room is ready for Master";
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Link DisConnected case: {cause}");
        textLabel.color = Color.red;
        textLabel.text = $"Link DisConnected case: {cause}";
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        //throw new System.NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        foreach (KeyValuePair<string,object> key in data)
        {
            Debug.Log($"key {key.Key} Значение {key.Value} ");
        }
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log(debugMessage);
    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName);
        Debug.Log(PhotonNetwork.CurrentRoom.Name);

    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        //throw new System.NotImplementedException();
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //throw new System.NotImplementedException();
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {
        throw new System.NotImplementedException();
    }
}
