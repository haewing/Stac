using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMng
{
    private static GameMng Ins;

    public static GameMng GetIns
    {
        get
        {
            if (null == Ins)
            {
                Ins = new GameMng();
            }
            return Ins;
        }
    }


    public LobbyInfo _LobbyInfo = new LobbyInfo();
    public InfoMng _GameInfo = new InfoMng();
    public LobbyInfo lobbyInfo { get { return _LobbyInfo; } }

    public byte PlayerAttackMode = 0; 


    public bool PlayerAttack = true;
    public float Speed = 10;
    public float SkillCoolTime = 0;
    public float KunaiCoolTime = 0;
    public float KnifeCoolTime = 0.5f;

    public bool PlayerGrapplingHitCheck = false;
    public bool PlayerCastleIn = false;

    public int  CameraMode = 0;

    public bool  BossClear = false;

    public float PlayTime = 0;
    public bool TitleMenwStageSelete = false;
    public void Init()
    {
        Speed = 10;
        KnifeCoolTime = 0.5f;

        PlayerGrapplingHitCheck = false;

        CameraMode = 0;

        BossClear = false;

        PlayTime = 0;
    }
}
