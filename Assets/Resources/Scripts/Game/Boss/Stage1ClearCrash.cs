using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1ClearCrash : MonoBehaviour
{
    [SerializeField] BoxCollider2D GameClear;
    [SerializeField] BoxCollider2D UnderIn;
    [SerializeField] BoxCollider2D BossClear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMng.GetIns.BossClear) BossClear.isTrigger = true;
        else  BossClear.isTrigger = false;


    }

}
