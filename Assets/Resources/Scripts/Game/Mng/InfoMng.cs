using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMng
{
    private static InfoMng Ins;

    public static InfoMng GetIns
    {
        get
        {
            if (null == Ins)
            {
                Ins = new InfoMng();
            }
            return Ins;
        }
    }

    public bool GameClear = false;
    public int TurretCount = 9;
    public float PlayerHP = 100;
    public float BossHP = 250;
    public float TBossHP = 150;
    public float BossHP2 = 300;






    public string StageName = null;
    public bool[] StageLock = {true,false,false };
    public bool[] Stage1Star = {false, false,false};
    public bool[] Stage2Star = {false, false,false};
    public bool[] Stage3Star = {false, false,false};

    public void init()
    {
        GameClear = false;
        TurretCount = 9;
         PlayerHP = 100;
         TBossHP = 150;
         BossHP = 200;
         BossHP2 = 250;
    }
}
