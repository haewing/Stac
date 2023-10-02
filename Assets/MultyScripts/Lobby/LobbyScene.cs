using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyScene : MonoBehaviourPunCallbacks
{
    public string GAME_VERSION = "1.0";
    public string LOBBY_NAME = "RedDot";

    [SerializeField] Text _txtState;

    [SerializeField] LoginDlg _LoginDlg;
    [SerializeField] RoomListDlg _RoomListDlg;

    [SerializeField] Button _btnJoin;
    [SerializeField] Button _btnCreate;
    [SerializeField] Button _btnOption;

    [SerializeField] InputField _inputRoomName;

    RoomListItem curRoomItem = null;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        _LoginDlg.InitDelegate(SetLogin);
    }

    private void Start()
    {
        _btnCreate.onClick.AddListener(ClickCreate);
        _btnJoin.onClick.AddListener(ClickJoin);
    }

    void ClickCreate()
    {
        string roomName = _inputRoomName.text;
        CreateRoom(roomName, 2);
    }

    void ClickJoin()
    {
        _btnCreate.interactable = false;
        _btnOption.interactable = false;

        JoinRoom();

        _btnCreate.interactable = true;
        _btnOption.interactable = true;
    }

    void ClickOption()
    {

    }

    void SetLogin(string _nickName)
    {
        PhotonNetwork.NickName = _nickName;
        PhotonNetwork.GameVersion = GAME_VERSION;
        PhotonNetwork.ConnectUsingSettings();

        _RoomListDlg.gameObject.SetActive(true);

        _txtState.text = "������ ������ ������...";
    }

    public override void OnConnectedToMaster()
    {
        _txtState.text = "������ ���� ���� �Ϸ�.";

        TypedLobby kLobby = new TypedLobby(LOBBY_NAME, LobbyType.Default);
        PhotonNetwork.JoinLobby(kLobby);

        _txtState.text = LOBBY_NAME + " �κ� ���� ��...";
    }

    public override void OnJoinedLobby()
    {
        _txtState.text = LOBBY_NAME + "\n�κ� ���� �Ϸ�.";
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("RoomScene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _txtState.text = "�κ�� ���Ӳ��� �ٽ� ������ �õ��մϴ�.";
        PhotonNetwork.ConnectUsingSettings();
    }

    void CreateRoom(string rName , byte maxPlayers)
    {
        if(PhotonNetwork.IsConnected)
        {
            if (string.IsNullOrEmpty(_txtState.text)) return;

            RoomOptions kOption = new RoomOptions();
            kOption.IsOpen = true;
            kOption.IsVisible = true;
            kOption.MaxPlayers = maxPlayers;
            kOption.CleanupCacheOnLeave = true;
            kOption.BroadcastPropsChangeToAll = true;

            kOption.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "roomState", (int)LobbyInfo.RoomState.isReady } };
            kOption.CustomRoomPropertiesForLobby = new string[] { "roomState" };

            TypedLobby kLobby = new TypedLobby(LOBBY_NAME, LobbyType.Default);
            PhotonNetwork.CreateRoom(rName, kOption, kLobby);
        }
    }

    void JoinRoom()
    {
        if(PhotonNetwork.IsConnected)
        {
            if(curRoomItem != null)
            {
                RoomInfo kRoomInfo = curRoomItem._RoomInfo;
                string rName = kRoomInfo.Name;
                int playerCount = kRoomInfo.PlayerCount;
                int state = (int)kRoomInfo.CustomProperties["roomState"];

                if(playerCount >= kRoomInfo.MaxPlayers)
                {
                    _txtState.text = "���� �� á���ϴ� \n�ٸ��濡 �����մϴ�.";
                    PhotonNetwork.JoinRandomRoom();
                    return;
                }

                if(state == (int)LobbyInfo.RoomState.isGame)
                {
                    _txtState.text = "���� �������Դϴ�\n�ٸ��濡 �����մϴ�.";
                    PhotonNetwork.JoinRandomRoom();
                    return;
                }

                PhotonNetwork.JoinRoom(rName);
                return;
            }
            else
            {
                _txtState.text = "���õ� ���� �����ϴ�\n�ٸ��濡 �����մϴ�";
                PhotonNetwork.JoinRandomRoom();
                return;
            }
        }
        else
        {
            _txtState.text = "�κ�� ���Ӳ��� �ٽ� ������ �õ��մϴ�.";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void SelectedItem(RoomListItem _item)
    {
        curRoomItem = _item;

        _inputRoomName.text = _item._RoomInfo.Name;
    }

    public void ShowRoomDlg()
    {
        _RoomListDlg.gameObject.SetActive(true);
    }
}
