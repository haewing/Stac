using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public PlayerController m_PlayerController;
    public AlphaMap m_Map;
    public void Initialize()
    {

    }
    public void InitializeUpdate()
    {
        m_PlayerController.InitializeUpdate();
    }
}
