using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBullet : MonoBehaviour
{
    [SerializeField] GameObject Effect;
    [HideInInspector]public float speed;
    [HideInInspector]public bool PT2 = false;
    Rigidbody2D rigid;
    private void OnDestroy()
    {

            GameObject Eff = Instantiate(Effect, transform.position, Quaternion.identity);

        
    }
    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }
 
    bool m = false;
    public void SlerpObj(Transform PlayerPos)
    {
        if (PlayerPos != null && !m)
        {
            
            Vector3 PlayerPosTarget = new Vector3(PlayerPos.position.x, PlayerPos.position.y - 4, PlayerPos.position.z);
            Vector3 dir = PlayerPosTarget - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            m = true;
        }
        speed = 15;

    }
    public void BulletInit(int Quaternion, float speed)
    {
        this.speed = speed;   
        gameObject.transform.eulerAngles = new Vector3(0, 0, Quaternion);
    }

    private void Update()
    {


        gameObject.transform.Translate(new Vector2(0, speed * Time.deltaTime));

        if (PT2)
        {
            SlerpObj(GameObject.Find("Boss").GetComponent<Boss2>().Pattern2_PlayerPos);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject Eff = Instantiate(Effect, transform.position, Quaternion.identity);
        }   
    }
}
