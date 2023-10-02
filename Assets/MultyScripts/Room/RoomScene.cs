using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomScene : MonoBehaviourPunCallbacks 
{
    public List<RoomPlayerSlot> ListOfRoomSlot = new List<RoomPlayerSlot>();

    [SerializeField] Text _txtRoomInfo;
    [SerializeField] Text _txtRoomName;
    [SerializeField] Text _txtRoomState;

    [SerializeField] Button _btnReady;
    [SerializeField] Button _btnExit;
    [SerializeField] Button _btnStart;

    private void Awake()
    {
        Initialize();

        _btnReady.onClick.AddListener(ClickReady);
        _btnExit.onClick.AddListener(ClickExit);
        _btnStart.onClick.AddListener(ClickStart);
    }

    void ClickReady()
    {
        LobbyInfo kLobby = GameMng.GetIns._LobbyInfo;
        Player myPlayer = kLobby.mySlotInfo.myPlayer;
        ExitGames.Client.Photon.Hashtable kHashTable = myPlayer.CustomProperties;

        int state = (int)kHashTable["userState"];
        if(state == (int)LobbyInfo.UserState.isReady)
        {
            kHashTable["userState"] = (int)LobbyInfo.UserState.isEnter;
        }
        else if(state == (int)LobbyInfo.UserState.isEnter)
        {
            kHashTable["userState"] = (int)LobbyInfo.UserState.isReady;
        }

        kLobby.mySlotInfo.myPlayer.SetCustomProperties(kHashTable);

        if(myPlayer.NickName == PhotonNetwork.NickName)
        {
            int newState = (int)kHashTable["userState"];
            FindRoomSlot(myPlayer.NickName).SetUserStateIMG(newState);
        }

        kLobby.mySlotInfo.myState = (int)kHashTable["userState"];
    }

    void ClickExit()
    {
        PhotonNetwork.LeaveRoom();
    }

    void ClickStart()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        LobbyInfo kLobby = GameMng.GetIns._LobbyInfo;
        Room kRoom = PhotonNetwork.CurrentRoom;
        for(int i = 0; i < kRoom.PlayerCount; i++)
        {
            Player myPlayer = kLobby.ListOfRoomSlot[i].myPlayer;
            ExitGames.Client.Photon.Hashtable kHashTable = myPlayer.CustomProperties;
            if ((int)kHashTable["userState"] == (int)LobbyInfo.UserState.isEnter)
            {
                _txtRoomState.text = "준비를 하지않은 플레이어가 있음.";
                return;
            }
        }

        ExitGames.Client.Photon.Hashtable RoomState = kRoom.CustomProperties;
        RoomState["roomState"] = (int)LobbyInfo.RoomState.isGame;
        kRoom.SetCustomProperties(RoomState);

        PhotonNetwork.LoadLevel("MultyGameScene");
    }

    void Initialize()
    {
        ClearRoomSlots();
        Room kRoom = PhotonNetwork.CurrentRoom;

        LobbyInfo kLobby = GameMng.GetIns._LobbyInfo;
        kLobby.Initialize(kRoom);

        UpdateToListRoomSLot();

        if(PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable kTable = kRoom.CustomProperties;
            kTable["roomState"] = (int)LobbyInfo.RoomState.isReady;
            kRoom.SetCustomProperties(kTable);
            kRoom.IsOpen = true;
        }

        UpdateRoomInfoTxt();
    }

    void UpdateToListRoomSLot()
    {
        LobbyInfo kLobby = GameMng.GetIns._LobbyInfo;
        int count = kLobby.ListOfRoomSlot.Count;
        for(int i = 0; i < count; i++)
        {
            RoomSlotInfo kSlotInfo = kLobby.ListOfRoomSlot[i];
            Player kPlayer = kSlotInfo.myPlayer;

            if (kPlayer != null)
            {
                ListOfRoomSlot[i].Initialize(kSlotInfo);

                if(kLobby.CheckIsMine(kPlayer.NickName))
                {
                    ListOfRoomSlot[i].showIsMine();
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        LobbyInfo info = GameMng.GetIns._LobbyInfo;
        RoomSlotInfo Room_Slot = info.GetEmptySlotInfo();

        Room_Slot.myPlayer = newPlayer;
        Room_Slot.myState = (int)LobbyInfo.UserState.isEnter;

        ListOfRoomSlot[Room_Slot.idxSlot - 1].Initialize(Room_Slot);

        UpdateToListRoomSLot();
        UpdateRoomInfoTxt(); 
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        LobbyInfo info = GameMng.GetIns._LobbyInfo;
        ExitGames.Client.Photon.Hashtable kHashTable = otherPlayer.CustomProperties;
        int index = (int)kHashTable["iRoomSlot"];
        RoomSlotInfo Room_Slot = info.GetSlotInfo(index);
        Room_Slot.Clear();

        ListOfRoomSlot[Room_Slot.idxSlot - 1].Clear();

        UpdateToListRoomSLot();
        UpdateRoomInfoTxt();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer.NickName == PhotonNetwork.NickName) return;

        int state = (int)targetPlayer.CustomProperties["userState"];
        RoomPlayerSlot slot = FindRoomSlot(targetPlayer.NickName);

        if(slot != null) slot.SetUserStateIMG(state);
    }

    void UpdateRoomInfoTxt()
    {
        Room kRoom = PhotonNetwork.CurrentRoom;
        _txtRoomInfo.text = string.Format("{0} / {1}", kRoom.PlayerCount, kRoom.MaxPlayers);
        _txtRoomName.text = kRoom.Name;
    }

    void ClearRoomSlots()
    {
        for (int i = 0; i < ListOfRoomSlot.Count; i++)
        {
            ListOfRoomSlot[i].Clear();
        }
    }

    RoomPlayerSlot FindRoomSlot(string name)
    {
        for (int i = 0; i < ListOfRoomSlot.Count; i++)
        {
            string str = ListOfRoomSlot[i].GetName();
            if(str == name)
            {
                return ListOfRoomSlot[i];
            }
        }

        return null;
    }
}