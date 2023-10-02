using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStars : MonoBehaviour
{
    [SerializeField] List<Star> stars = new List<Star>();
    [SerializeField] StageSelect StageSelect;
    public void PlayStarAnimation(int starCount, int stage)
    {
        StartCoroutine(starAnimate(starCount, stage));
    }

    IEnumerator starAnimate(int starCount, int stage)
    {

        yield return new WaitForSeconds(3.2f);
        for (int i = (stage * 3) - 3; i < (stage * 3) - 3 + starCount ; i++)
        {
            stars[i].Animate();
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.5f);
        {
            switch (stage)
            {
                case 1:
                    StageSelect.StageH("stage2", 1, stage + 1);
                    break;
                case 2:
                    StageSelect.StageH("stage3", 1, stage + 1);
                    break;
            }
        }
        
    }
}
