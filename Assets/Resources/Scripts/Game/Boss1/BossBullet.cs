using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] GameObject Effect;
    [HideInInspector]public float speed;
    [HideInInspector]public bool PT2 = false;

    private void OnDestroy()
    {

            GameObject Eff = Instantiate(Effect, transform.position, Quaternion.identity);
            
    }
    
    public void SlerpObj(Transform PlayerPos)
    {
        gameObject.transform.position = Vector3.Slerp(transform.position, PlayerPos.position, 1f * Time.deltaTime);
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
        }   
    }
}
