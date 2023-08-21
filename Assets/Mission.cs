using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField] Animator m_MisstionAni;
    bool MisstionAniCheak = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_MisstionAni.SetTrigger("In");
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            m_MisstionAni.SetTrigger("Out");
        }
    }
    public void MissionIn()
    {
        Debug.Log("a");
        for (int i = 0; i < 1; i++)
        {
            if (!MisstionAniCheak)
            {
                m_MisstionAni.SetTrigger("In");
                MisstionAniCheak = true;
                break;
            }
            if (MisstionAniCheak)
            {
                m_MisstionAni.SetTrigger("Out");
                MisstionAniCheak = false;
                break;
            }

        }

    }
}
