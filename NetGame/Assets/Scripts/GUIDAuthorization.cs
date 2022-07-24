using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class GUIDAuthorization : MonoBehaviour
{
    
    [SerializeField] private Button _buttonGoToGame;
    [SerializeField] private Text _statusInfo;
    private const string PlayerFab_ID = "5E23E";
    private string _game_version = "Dev";
    private const string AuthKey = "AythKey";
    [SerializeField]public GameObject panelController;
    [SerializeField] public GameObject UserPanelInfo;
    [SerializeField] public CatalogManager catalog;
    public struct Data
    {
        public string id;
        public bool flagNeedCreation;

    }
    
   
    private void Awake()
    {
        _buttonGoToGame.onClick.AddListener(SignGUID);
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
          PlayFabSettings.staticSettings.TitleId = PlayerFab_ID;
        }
        
    }

    public void SignGUID()
    {
        PanelOn();
        
        var needcreation = PlayerPrefs.HasKey(AuthKey);
        var iD = PlayerPrefs.GetString(AuthKey, Guid.NewGuid().ToString());
        Data data = new Data();
        data.id = iD;
        data.flagNeedCreation = needcreation;
        var request = new LoginWithCustomIDRequest
        {
            CustomId = iD,
            CreateAccount = !needcreation

        };

        PlayFabClientAPI.LoginWithCustomID(request,Success,Fail,data);
        gameObject.SetActive(false);
        Invoke("PanelOff", 5f);
        
    }

    private void Success(LoginResult result)
    {

        PlayerPrefs.SetString(AuthKey, ((Data)result.CustomData).id);
        if(((Data)result.CustomData).flagNeedCreation)
        {
            _statusInfo.color = Color.black;
            string info = "Быстрая авторизация.Создание нового ID не требуется";
            var textId = ((Data)result.CustomData).id;
            Debug.Log($"GUID authorization has done");
            Debug.Log($"GUID is {textId}");
            _statusInfo.text = $"GUID authorization has done { info }";
            PrintInfo(textId);
            catalog.GetCatalog();
        }
        else
        {
            _statusInfo.color = Color.black;
            string info = "Быстрая авторизация.Требуется создание нового ID ";
            var textId = ((Data)result.CustomData).id;
            Debug.Log($"GUID authorization has done");
            Debug.Log($"GUID is {textId}");
            _statusInfo.text = $"GUID authorization has done { info }";
        }
        
    }

    private void Fail(PlayFabError error)
    {
        
        _statusInfo.color = Color.red;
        var errorMesage = error.GenerateErrorReport();
        Debug.LogError($"GUID authorization is failed: {errorMesage}");
        _statusInfo.text = $"GUID authorization is failed: {errorMesage}";
    }

    public void PanelOn()
    {
        panelController.SetActive(true);
    }
    public void PanelOff()
    {
        panelController.SetActive(false);
    }

    public void PrintInfo(string info)
    {
        UserPanelInfo.SetActive(true);
        var text = UserPanelInfo.GetComponent<Text>();
        Debug.LogWarning($"в игру зашел{info}");
        //text.text = $"В игру зашел игрок {info} ";//почему тут налл референс эксепшн???
    }
}
