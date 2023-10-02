using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    public RoomInfo _RoomInfo;
    [SerializeField] Image _imgBtn;
    [SerializeField] Text _txtRoomInfo;
    [SerializeField] Text _txtRoomState;

    public void Initialize(RoomInfo _info)
    {
        _RoomInfo = _info;

        ClearSelected();
        _txtRoomInfo.text = string.Format("{0} {1}/{2}", _RoomInfo.Name, _RoomInfo.PlayerCount, _RoomInfo.MaxPlayers);

        ExitGames.Client.Photon.Hashtable kHashTable = _info.CustomProperties;
        int kState = (int)kHashTable["roomState"];
        _txtRoomState.text = GetStateString(kState);
    }

    string GetStateString(int state)
    {
        return (state == (int)LobbyInfo.RoomState.isGame) ? "Game" : "Ready";
    }

    public void SelectedItem()
    {
        _imgBtn.color = Color.green;
    }

    public void ClearSelected()
    {
        _imgBtn.color = Color.white;
    }
}
