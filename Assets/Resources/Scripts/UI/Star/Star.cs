using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] Animator starAnimator = null;

    public void Animate()
    {
        starAnimator.SetTrigger("Animate");
    }
}
