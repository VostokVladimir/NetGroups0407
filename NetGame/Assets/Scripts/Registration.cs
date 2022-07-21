using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Registration : MonoBehaviour
{
    [SerializeField] private InputField _userEmailField;
    [SerializeField] private InputField _userNameField;
    [SerializeField] private InputField _userPasswordField;
    [SerializeField] private Button _buttonGoTOSignIn;
    [SerializeField] private Button _buttonGreateAccount;
    [SerializeField] private Text _statusInfo;
    private const string PlayerFab_ID = "5E23E";
    private string _game_version = "Dev";
    public bool registrationstatus;
    [SerializeField] public GameObject panelController;
    private string _userName;
    private string _userEmail;
    private string _userPassword;
   


    private void Awake()
    {
        _userNameField.onValueChanged.AddListener(SetUserName);
        _userEmailField.onValueChanged.AddListener(SetUserEmail);
        _userPasswordField.onValueChanged.AddListener(SetUserPassword);
        _buttonGreateAccount.onClick.AddListener(CreateAccount);
        _buttonGoTOSignIn.onClick.AddListener(SignIn);





    }

    private void Start()
    {
        var titleId = PlayFabSettings.staticSettings.TitleId;
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
           PlayFabSettings.staticSettings.TitleId=PlayerFab_ID;
     

    }

    private void SetUserName(string value)
    {
        _userName = value;
    }
    private void SetUserEmail(string value)
    {
        _userEmail = value;
    }
    private void SetUserPassword(string value)
    {
        _userPassword = value;
    }

    public void CreateAccount()
    {
        PanelOn();
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _userName,
            Email = _userEmail,
            Password = _userPassword,
            RequireBothUsernameAndEmail = true
        },
        Result,
        erorr => 
        {
            _statusInfo.text = erorr.ErrorMessage;
            Debug.LogError($"Sorry!Registration Fail {erorr}");// СТРОКА 70
            registrationstatus = false;
            
        });
      
        Invoke("PanelOff", 5f);
    }

    private void Result(RegisterPlayFabUserResult result)
    {
        Debug.Log($"Succsess registration {_userName}");
        _statusInfo.text = $"Succsess registration {_userName}";
        //меняем статус что бы закрыть форму greate account на сцене
        registrationstatus = true;
        Debug.Log(registrationstatus);

    }
    private void SignIn()
    {

        registrationstatus = true;
        
       Debug.Log(registrationstatus);
        
    }

    public void PanelOn()
    {
        panelController.SetActive(true);
    }
    public void PanelOff()
    {
        panelController.SetActive(false);
    }

}


