using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{

    public GameObject LookCamera;
    public Animator Ani;
    public GameObject RedBullet;
    public GameObject Laser;
    public GameObject SkillBullet;
    [HideInInspector] public float m_EnemyHP = 5;

    Vector3 targetPos = Vector3.zero;

    float speed = 5;

    bool Att = true;
    int AttackCount = 0;



    public void Look()
    {
        GameObject target = GameObject.Find("Player");
        if (target != null)
        {
            Vector2 direction = new Vector2(
                LookCamera.transform.position.x - target.transform.position.x,
                LookCamera.transform.position.y - target.transform.position.y + 2
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            LookCamera.transform.rotation = angleAxis;
        }


    }
    public void MovePettern(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
    public void Update()
    {
        if (m_EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if(transform.position == targetPos && Att)
        {
            Att = false;
            Ani.SetTrigger("IsIdle");
            StartCoroutine("CreateBullet");
        }
        Look();
        

    }
    public void Attact()
    {
        GameObject Bullet = Instantiate(RedBullet, LookCamera.transform.position, Quaternion.identity);
        Bullet.GetComponent<DroneBullet>().Launch(3);
        Destroy(Bullet, 10);
    }
    public void Skill()
    {
        GameObject Bullet = Instantiate(SkillBullet, LookCamera.transform.position, Quaternion.identity);
        Bullet.GetComponent<DroneBullet>().Launch(100);
        Destroy(Bullet, 10);
    }

    private IEnumerator CreateBullet()
    {
        
        while (true)
        {
            if (AttackCount < 3)
            {
                Attact();
                AttackCount++;
                
            }
            if (AttackCount == 3)
            {
                Laser.SetActive(true);
                yield return new WaitForSeconds(2.0f);
                Laser.SetActive(false);
                Skill();
                AttackCount = 0;
            }
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(1);
    }


}
