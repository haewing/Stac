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

        _txtState.text = "마스터 서버에 접속중...";
    }

    public override void OnConnectedToMaster()
    {
        _txtState.text = "마스터 서버 접속 완료.";

        TypedLobby kLobby = new TypedLobby(LOBBY_NAME, LobbyType.Default);
        PhotonNetwork.JoinLobby(kLobby);

        _txtState.text = LOBBY_NAME + " 로비에 접속 중...";
    }

    public override void OnJoinedLobby()
    {
        _txtState.text = LOBBY_NAME + "\n로비에 접속 완료.";
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("RoomScene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _txtState.text = "로비와 접속끊김 다시 연결을 시도합니다.";
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
                    _txtState.text = "방이 꽉 찼습니다 \n다른방에 연결합니다.";
                    PhotonNetwork.JoinRandomRoom();
                    return;
                }

                if(state == (int)LobbyInfo.RoomState.isGame)
                {
                    _txtState.text = "방이 게임중입니다\n다른방에 접속합니다.";
                    PhotonNetwork.JoinRandomRoom();
                    return;
                }

                PhotonNetwork.JoinRoom(rName);
                return;
            }
            else
            {
                _txtState.text = "선택된 방이 없습니다\n다른방에 접속합니다";
                PhotonNetwork.JoinRandomRoom();
                return;
            }
        }
        else
        {
            _txtState.text = "로비와 접속끊김 다시 연결을 시도합니다.";
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
