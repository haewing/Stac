using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageStarUI : MonoBehaviour
{
    [SerializeField] Image[] Star; 
    

    // Update is called once per frame
    void Update()
    {

        StarCheck();
        Miss();
    }

    public void StarCheck()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage1":
                for (int i = 0; i < InfoMng.GetIns.Stage1Star.Length; i++)
                {
                    Star[i].color = InfoMng.GetIns.Stage1Star[i] ? new Color(0.5f, 0.5f, 0.5f, 0.3f) : new Color(1,1,0,1);
                }
                break;
            case "Stage2":
                for (int i = 0; i < InfoMng.GetIns.Stage2Star.Length; i++)
                {
                    Star[i].color = InfoMng.GetIns.Stage2Star[i] ? new Color(0.5f, 0.5f, 0.5f, 0.3f) : new Color(1, 1, 0, 1);
                }
                break;
            case "Stage3":
                for (int i = 0; i < InfoMng.GetIns.Stage2Star.Length; i++)
                {
                    Star[i].color = InfoMng.GetIns.Stage3Star[i] ? new Color(0.5f, 0.5f, 0.5f, 0.3f) : new Color(1, 1, 0, 1);
                }
                break;
        }
    }
    public void Miss()
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage1":
                InfoMng.GetIns.Stage1Star[0] = GameMng.GetIns.BossClear ? false : true;
                InfoMng.GetIns.Stage1Star[1] = GameMng.GetIns.PlayTime > 180 ? true : false;
                InfoMng.GetIns.Stage1Star[2] = InfoMng.GetIns.PlayerHP < 50 ? true : false;
                break;
            case "Stage2":
                InfoMng.GetIns.Stage2Star[0] = GameMng.GetIns.BossClear ? false : true;
                InfoMng.GetIns.Stage2Star[1] = GameMng.GetIns.PlayTime > 180 ? true : false;
                InfoMng.GetIns.Stage2Star[2] = InfoMng.GetIns.PlayerHP < 50 ? true : false;
                break;
            case "Stage3":
                InfoMng.GetIns.Stage3Star[0] = GameMng.GetIns.BossClear ? false : true;
                InfoMng.GetIns.Stage3Star[1] = GameMng.GetIns.PlayTime > 180 ? true : false;
                InfoMng.GetIns.Stage3Star[2] = InfoMng.GetIns.PlayerHP < 50 ? true : false;
                break;
        }

    }
}
