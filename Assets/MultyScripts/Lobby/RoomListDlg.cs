using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListDlg : MonoBehaviourPunCallbacks
{
    [SerializeField] LobbyScene _LobbyScene;
    [SerializeField] ScrollRect _Scroll;
    [SerializeField] GameObject _prefabRoomItem;
    [HideInInspector] public List<RoomListItem> ListOfRoomItem = new List<RoomListItem>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            RoomInfo kInfo = roomList[i];
            string name = "";

            if(i < ListOfRoomItem.Count)
            {
                if (ListOfRoomItem[i]._RoomInfo != null) name = ListOfRoomItem[i]._RoomInfo.Name;
            }
            
            int index = ListOfRoomItem.FindIndex(x => name == kInfo.Name);

            if (kInfo.RemovedFromList)
            {
                if (index != -1) Destroy(ListOfRoomItem[i].gameObject);
            }
            else
            {
                if (index == -1) CreateOnScroll(kInfo);
                else if (index != -1) UpdateOnListItem(kInfo, index);
            }
        }
    }

    void CreateOnScroll(RoomInfo _CreateInfo)
    {
        GameObject go = Instantiate(_prefabRoomItem, _Scroll.content);
        RoomListItem kItem = go.GetComponent<RoomListItem>();
        kItem.Initialize(_CreateInfo);

        go.GetComponent<Button>().onClick.AddListener(() =>
        {
            _LobbyScene.SelectedItem(kItem);
            ClearListOnItemColor();
            kItem.SelectedItem();
        }
        );

        ListOfRoomItem.Add(kItem);
    }

    void UpdateOnListItem(RoomInfo _updateInfo , int updateIndex)
    {
        ListOfRoomItem[updateIndex].Initialize(_updateInfo);
    }

    void ClearListOnItemColor()
    {
        for(int i = 0; i < ListOfRoomItem.Count; i++)
        {
            ListOfRoomItem[i].ClearSelected();
        }
    }
}
