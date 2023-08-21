using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InfoMng.GetIns.TurretCount = transform.childCount;
        if (transform.childCount == 0)
        {
            InfoMng.GetIns.GameClear = true;
        }
    }
}
