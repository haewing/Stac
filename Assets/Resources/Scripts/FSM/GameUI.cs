using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameUI : MonoBehaviour
{
    MultyPlayerControll m_PlayerController;
    public AlphaMap m_Map;
    public void Initialize()
    {
        GameObject go = PhotonNetwork.Instantiate("Prefabs/Player", Vector2.zero, Quaternion.identity);
        m_PlayerController = go.GetComponent<MultyPlayerControll>();
    }
    public void InitializeUpdate()
    {
        m_PlayerController.InitializeUpdate();
    }
}
