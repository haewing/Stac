using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStars : MonoBehaviour
{
    [SerializeField] List<Star> stars = new List<Star>();

    public void PlayStarAnimation(int starCount, int stage)
    {
        StartCoroutine(starAnimate(starCount, stage));
    }

    IEnumerator starAnimate(int starCount, int stage)
    {
        for (int i = (stage * 3) - 3; i < (stage * 3) - 3 + starCount ; i++)
        {
            stars[i].Animate();
            yield return new WaitForSeconds(0.25f);
        }
    }
}
