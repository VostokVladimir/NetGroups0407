﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class SignInAuthorization : MonoBehaviour
{
   
    [SerializeField] private InputField _userNameField;
    [SerializeField] private InputField _userPasswordField;
    [SerializeField] private Button _buttonDone;
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonSignIn;
    [SerializeField] private Text _statusInfo;
    private const string PlayerFab_ID = "5E23E";
    private string _game_version = "Dev";
    [SerializeField] public GameObject panelController;
    private string _userName;
    private string _userPassword;

    private void Awake()
    {
        _userNameField.onValueChanged.AddListener(SetUserName);
        _userPasswordField.onValueChanged.AddListener(SetUserPassword);
        _buttonSignIn.onClick.AddListener(SignIn);
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = PlayerFab_ID;

    }

    private void SetUserName(string value)
    {
        _userName = value;
    }
    
    private void SetUserPassword(string value)
    {
        _userPassword = value;
    }

    public void SignIn()
    {
        PanelOn();
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _userName,
            Password = _userPassword
        }, result =>
        {
            Debug.Log($"Succsess SignIn {_userName}");
            Debug.Log($"User entered {result.LastLoginTime}");
            _statusInfo.text = $"Succsess SignIn {_userName}";
        }, erorr =>
        {
            Debug.LogError($"User not SignIn :{erorr.ErrorMessage}");
            _statusInfo.text = erorr.ErrorMessage;

        });
        Invoke("PanelOff", 5f);
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
