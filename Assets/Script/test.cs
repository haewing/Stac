using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Backgroundmove[] speedtest;
    public float speed;
    public float speedoffset;

    public bool on = false;

    public float[] m1;
    void Update()
    {
        if (on)
        {
            for (int i = 0; i < speedtest.Length; i++)
            {
                speedtest[i].speed = speed + (speedoffset*i);
            }
        }
        else
        {
            for (int i = 0; i < speedtest.Length; i++)
            {
                speedtest[i].speed = m1[i];
            }
        }
    }
}
