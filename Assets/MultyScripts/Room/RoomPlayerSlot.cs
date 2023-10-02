using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RoomPlayerSlot : MonoBehaviour
{
    [SerializeField] GameObject _imgIsMine;
    [SerializeField] GameObject _imgIsMaster;
    [SerializeField] Image _imgIsReady;

    [SerializeField] Text _txtPlayerName;
    [SerializeField] Text _txtUserState;

    public void Initialize(RoomSlotInfo kInfo)
    {
        string nickName = kInfo.myPlayer.NickName;

        _txtPlayerName.text = nickName;
        int state = kInfo.myState;
        SetUserStateIMG(state);

        if (kInfo.myPlayer.IsMasterClient) ShowIsMaster();
        else HideIsMaster();
    }

    public void Clear()
    {
        _txtPlayerName.text = "none";
        _txtUserState.text = LobbyInfo.UserState.isNone.ToString();

        HideIsMaster();
        HideIsMine();
    }

    public void SetUserStateIMG(int state)
    {
        if(state == (int)LobbyInfo.UserState.isEnter)
        {
            HideReadyIMG();
            _txtUserState.text = "Enter";
        }
        else if(state == (int)LobbyInfo.UserState.isReady)
        {
            ShowReadyIMG();
            _txtUserState.text = "Ready";
        }    
        else
        {
            HideReadyIMG();
            _txtUserState.text = "IsNone";
        }
    }

    public string GetName()
    {
        return _txtPlayerName.text;
    }

    void ShowReadyIMG()
    {
        _imgIsReady.gameObject.SetActive(true);
    }

    void HideReadyIMG()
    {
        _imgIsReady.gameObject.SetActive(false);
    }

    public void showIsMine()
    {
        _imgIsMine.SetActive(true);
    }

    public void HideIsMine()
    {
        _imgIsMine.SetActive(false);
    }

    public void ShowIsMaster()
    {
        _imgIsMaster.SetActive(true);
    }

    public void HideIsMaster()
    {
        _imgIsMaster.SetActive(false);
    }
}
