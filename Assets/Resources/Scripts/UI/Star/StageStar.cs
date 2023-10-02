using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStar : MonoBehaviour
{
    [SerializeField] StageStars stage1 = null;
    // Update is called once per frame

    int _star = 0;
    public void Update()
    {
        switch (InfoMng.GetIns.StageName)
        {
            case "Stage1":
                for (int i = 0; i < InfoMng.GetIns.Stage1Star.Length; i++)
                {
                    if (!InfoMng.GetIns.Stage1Star[i]) _star++;
                }
                StageStarCount(_star, 1);
                break;
            case "Stage2":
                for (int i = 0; i < InfoMng.GetIns.Stage2Star.Length; i++)
                {
                    if (!InfoMng.GetIns.Stage2Star[i])_star++;
                }
                StageStarCount(_star, 2);
                break;
            case "Stage3":
                for (int i = 0; i < InfoMng.GetIns.Stage3Star.Length; i++)
                {
                    if (!InfoMng.GetIns.Stage3Star[i]) _star++;
                }
                StageStarCount(_star, 3);
                break;
        }


    }
    
    public void StageStarCount(int StarCount, int Stage)
    {
        stage1.PlayStarAnimation(StarCount, Stage);
        _star = 0;
        InfoMng.GetIns.StageName = null;

    }
}
