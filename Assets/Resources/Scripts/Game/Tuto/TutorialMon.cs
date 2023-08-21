using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMon : MonoBehaviour
{
    [HideInInspector] public float m_EnemyHP = 5;
    public GameObject Set;
    public GameObject Set1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_EnemyHP <= 0)
        {
            Set.SetActive(true);
            Set1.SetActive(false);
            Destroy(gameObject);
        }
    }
}
