using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomSlotInfo
{
    public int idxSlot = 0;
    public Player myPlayer = null;
    public int myState = 0;

    public void Clear()
    {
        myState = (int)LobbyInfo.UserState.isNone;
        myPlayer = null;
    }
}

public class LobbyInfo
{
    public enum RoomState
    {
        isReady,
        isGame,
    }

    public enum UserState
    {
        isNone,
        isEnter,
        isReady,
    }

    public List<RoomSlotInfo> ListOfRoomSlot = new List<RoomSlotInfo>();
    public RoomSlotInfo mySlotInfo = null;
    
    public void Initialize(Room kRoom)
    {
        RoomPlayer_Initialize(kRoom);
    }

    void RoomPlayer_Initialize(Room kRoom)
    {
        ListOfRoomSlot.Clear(); 
        
        for(int i = 0; i < kRoom.MaxPlayers; i++)
        {
            RoomSlotInfo kInfo = new RoomSlotInfo();
            kInfo.idxSlot = i + 1;
            ListOfRoomSlot.Add(kInfo);
        }

        int nCount = 0;
        Player MyPlayer = null;
        Dictionary<int, Player> kPlayers = kRoom.Players;

        foreach(KeyValuePair<int , Player> pair in kPlayers)
        {
            Player kPlayer = pair.Value;

            if (nCount < kRoom.MaxPlayers )
            {
                if (kPlayer.NickName == PhotonNetwork.NickName)
                {
                    MyPlayer = kPlayer;
                }
                else
                {
                    ExitGames.Client.Photon.Hashtable kHashTable = kPlayer.CustomProperties;
                    int index = (int)kHashTable["iRoomSlot"];

                    RoomSlotInfo info = GetSlotInfo(index);
                    if (info != null)
                    {
                        info.myPlayer = kPlayer;
                        info.myState = (int)kHashTable["userState"];
                    }
                }
            }
            nCount++;
        }

        SetMyRoomSlot(MyPlayer);
    }

    void SetMyRoomSlot(Player kPlayer)
    {
        RoomSlotInfo info = GetEmptySlotInfo();
        info.myPlayer = kPlayer;
        info.myState = (int)UserState.isEnter;

        ExitGames.Client.Photon.Hashtable kHashTable = info.myPlayer.CustomProperties;

        if (kHashTable == null)
            kHashTable = new ExitGames.Client.Photon.Hashtable();

        kHashTable.Clear();

        kHashTable.Add("iRoomSlot", info.idxSlot);
        kHashTable.Add("userState", (int)UserState.isEnter);

        kPlayer.SetCustomProperties(kHashTable);

        mySlotInfo = info;
    }

    public RoomSlotInfo GetEmptySlotInfo()
    {
        for (int i = 0; i < ListOfRoomSlot.Count; i++)
        {
            int state = ListOfRoomSlot[i].myState;

            if (state == (int)UserState.isNone)
            {
                return ListOfRoomSlot[i];
            }
        }

        return null;
    }

    public RoomSlotInfo GetSlotInfo(int index)
    {
        if (index > 0 && index <= ListOfRoomSlot.Count)
            return ListOfRoomSlot[index-1];
        return null;
    }

    public bool CheckIsMine(string cName)
    {
        if (cName == PhotonNetwork.NickName) return true;
        else return false;
    }
}
