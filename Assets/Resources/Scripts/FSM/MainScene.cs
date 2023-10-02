using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public GameUI m_GameUI;
    public HudUI m_HudUI;
    public Texture2D m_CursorImg;

    private BattleFSM m_BattleFSM = new BattleFSM();
    // Start is called before the first frame update
    private void Awake()
    {
        m_BattleFSM.Initalize(Ready, Game, Wave, Result);
        m_BattleFSM.SetReadyState();

        GameMng.GetIns.Init();
        InfoMng.GetIns.init();
        Cursor.SetCursor(m_CursorImg, new Vector2(250,250), CursorMode.Auto);
    }
    void Ready()
    {
        m_BattleFSM.SetGameState();
        m_GameUI.Initialize();
        m_HudUI.Initialize();

    }
    void Game()
    {



    }
    void Wave()
    {




    }
    void Result()
    {




    }
    // Update is called once per frame
    void Update()
    {
        if(m_BattleFSM != null)
            m_BattleFSM.OnUpdatae();

        if (m_BattleFSM.IsGameState())
        {
            m_GameUI.InitializeUpdate();
            m_HudUI.InitializeUp();


            if(InfoMng.GetIns.PlayerHP != 0)
            {
                GameMng.GetIns.PlayTime += Time.deltaTime;
            }

        }
    }

}
