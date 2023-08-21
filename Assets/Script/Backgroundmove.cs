using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Backgroundmove : MonoBehaviour
{
    public PlayerController a;
    public float speed;

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 curPos = transform.position;
        Vector3 nextleftPos = Vector3.left * (a.speed * speed) * Time.deltaTime;
        Vector3 nextupPos = Vector3.up * (a.speed * speed) * Time.deltaTime;
        transform.position = curPos + nextleftPos + (nextupPos * -speed);
    }
}