using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMon : MonoBehaviour
{

    [SerializeField] GameObject set;
    [SerializeField] GameObject set1;
    // Start is called before the first frame update


    private void OnDestroy()
    {
        set.SetActive(true);
        set1.SetActive(false);
    }
}
