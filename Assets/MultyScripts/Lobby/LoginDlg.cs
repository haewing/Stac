using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LoginDlg : MonoBehaviour
{
    [SerializeField] LobbyScene _LobbyScene;
    [SerializeField] GameObject LoginDlgObject;
    [SerializeField] Button _btnLogin;
    [SerializeField] Button _btnExit;

    [SerializeField] InputField _inputNickName;

    public delegate void DelegateFunc(string str);
    public DelegateFunc _DelegateFunc = null;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        _btnExit.onClick.AddListener(ClickExit);
        _btnLogin.onClick.AddListener(ClickStart);
    }

    void Initialize()
    {
        string str = PhotonNetwork.NickName;

        if (string.IsNullOrEmpty(str)) LoginDlgObject.SetActive(true);
        else
        {
            LoginDlgObject.SetActive(false);
            _LobbyScene.ShowRoomDlg();
        }
    }
    
    public void InitDelegate(DelegateFunc _func)
    {
        _DelegateFunc = _func;
    }

    void ClickStart()
    {
        if(_DelegateFunc != null)
        {
            if (string.IsNullOrEmpty(_inputNickName.text)) return;

            string str = _inputNickName.text;
            _DelegateFunc(str);
            LoginDlgObject.SetActive(false);
        }
    }

    void ClickExit()
    {
        Application.Quit();
    }
}
