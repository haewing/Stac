using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    GameObject PPos;
    Rigidbody2D rbody;
    GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        // 해당 방향으로의 속도를 세팅하기
        //x축을 넘어가면 반전하기

        //GameObject target = GameObject.Find("Player");
        //transform.LookAt(target.transform);
        //gameObject.transform.Translate(Vector3.up * Time.deltaTime * 10);
    }
    public void Launch(float Speed)
    {
        GameObject target = GameObject.Find("Player");
        if (target != null)
        {
            Vector2 direction = new Vector2(
               transform.position.x - target.transform.position.x,
               transform.position.y - target.transform.position.y
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 10 * Time.deltaTime);
            rotation.z += 180;
            transform.rotation = angleAxis; 
        }
        targetObject = GameObject.Find("Player");
        rbody = GetComponent<Rigidbody2D>();
        Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;
        float vx = dir.x * Speed;
        float vy = dir.y * Speed;
        rbody.velocity = new Vector2(vx, vy);
    }
    public void Las(Transform target)
    {
        if (target != null)
        {
            Vector2 direction = new Vector2(
               transform.position.x - target.transform.position.x,
               transform.position.y - target.transform.position.y
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 10 * Time.deltaTime);
            rotation.z += 180;
            transform.rotation = angleAxis;
        }
        
        rbody = GetComponent<Rigidbody2D>();
        Vector3 dir = (target.position - this.transform.position).normalized;
        float vx = dir.x * 500;
        float vy = dir.y *500;
        rbody.velocity = new Vector2(vx, vy);
    }
}
