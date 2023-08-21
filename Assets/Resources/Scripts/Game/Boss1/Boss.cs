using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GrapplingRope m_GrapplingRope;
    [SerializeField] CameraShake m_CameraShake;
    [SerializeField] GameObject m_BossBullet;
    [SerializeField] GameObject m_BossFloor;
    [SerializeField] Transform m_Perent;
    [SerializeField] Transform[] m_WeaknessPos;
    [SerializeField] GameObject m_Scope;
    [SerializeField] Scope m_ScopeScript;
    [SerializeField] Transform[] BoderPos;
    [SerializeField] GameObject m_Laser;
    [SerializeField] GameObject m_LaserBullet;
    [SerializeField] SoundMng m_Sound;

    [HideInInspector]public  Animator m_Ani;

    float QuaternionZ;
    bool IsPt2;
    private void Update()
    {
        QuaternionZ += Time.deltaTime * 10;
        m_Perent.eulerAngles = new Vector3(0, 0, QuaternionZ);
    }
    IEnumerator BossClear()
    {
        while (gameObject != null)
        {
            if (InfoMng.GetIns.BossHP <= 0)
            {
                InfoMng.GetIns.BossHP = 0;
                GameMng.GetIns.CameraMode = 3;
                yield return new WaitForSeconds(1);
                m_Ani.gameObject.GetComponent<Animator>().enabled = true;
                m_Ani.SetTrigger("IsDeath");
                m_Sound.Sound_Boss(2, false, false).Play();

                Transform[] childList = m_Perent.GetComponentsInChildren<Transform>();

                if (childList != null)
                {
                    for (int i = 1; i < childList.Length; i++)
                    {
                        if (childList[i] != transform)
                            Destroy(childList[i].gameObject);
                    }
                }

                yield return new WaitForSeconds(2f);
                GameMng.GetIns.CameraMode = 0;
                Destroy(gameObject);
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        m_Ani = gameObject.GetComponent<Animator>();
        StartCoroutine("BossPattern");
        StartCoroutine("BossWeaknessCreate");
        StartCoroutine("Rusid");
        StartCoroutine("BossClear");
    }

    void BossPattern1() // 무작위 발판
    {
        StartCoroutine("Pattern1");
    }
    IEnumerator Pattern1()
    {
        float x = Random.Range(BoderPos[0].position.x, BoderPos[1].position.x);
        float y = Random.Range(BoderPos[0].position.y, BoderPos[1].position.y);


        Vector2 RandomVector = new Vector2(x,y);
        GameObject m_Floor = Instantiate(m_BossFloor, RandomVector, Quaternion.identity);
        Destroy(m_Floor, 15);

        yield return new WaitForSeconds(2);
        m_Ani.SetTrigger("IsTp");
        m_Sound.Sound_Boss(1, false, false).Play();
        yield return new WaitForSeconds(0.2f);
        transform.position = new Vector2(m_Floor.transform.position.x + 1, m_Floor.transform.position.y + 8);
    }
    void BossPattern2() // 동방프로젝트
    {
        StartCoroutine("Pattern2");
    }
    IEnumerator Pattern2()
    {
        if (IsPt2)
        {
            for (int i = 0; i < 3; i++)
            {
                if (IsScopeHit) { break; }
                for (int j = 0; j < 45; j++)
                {
                    if (IsScopeHit) { break;  }
                    GameObject Bullet = Instantiate(m_BossBullet, transform.position, Quaternion.identity, m_Perent);
                    Bullet.GetComponent<BossBullet>().BulletInit((j * 8), 10);
                    yield return new WaitForSeconds(0.02f);
                    IsPt2 = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (IsScopeHit) { break; }
                yield return new WaitForSeconds(0.8f);
                for (int j = 0; j < 45; j++)
                {
                    if (IsScopeHit) { break; }
                    GameObject Bullet = Instantiate(m_BossBullet, transform.position, Quaternion.identity, m_Perent);
                    Bullet.GetComponent<BossBullet>().BulletInit((j * 8), 15);
                    IsPt2 = true;
                }
            }
        }
    }
    IEnumerator Rusid()
    {
        while (gameObject != null)
        {
            yield return new WaitForSeconds(20);
            BossPattern3();
        }
    }
    void BossPattern3() // 루시드 강공
    {
        StartCoroutine(Pattern3());
    }
    IEnumerator Pattern3()
    {
        GameObject P = GameObject.Find("Player");
        for (float i = 0; i < 10; i++)
        {
            GameObject Laser = Instantiate(m_Laser, new Vector2(P.transform.position.x, P.transform.position.y-3), Quaternion.identity);
            float z = Random.Range(0, 360);
            Laser.transform.eulerAngles = new Vector3(0, 0, z);
            Destroy(Laser, 1);
            yield return new WaitForSeconds(1f - (i/ 10));
        }
    }

    void BossWeakness()
    {
        if (!m_Scope.activeInHierarchy)
        {
            m_Scope.SetActive(true);
            int idx = Random.Range(0, m_WeaknessPos.Length);
            m_Scope.transform.position = m_WeaknessPos[idx].position;
        }
        //GameObject Scope = Instantiate(m_Scope, m_WeaknessPos[idx].position, Quaternion.identity);
        
    }

    IEnumerator BossWeaknessCreate()
    {
        while (gameObject != null)
        {
            BossWeakness();
            yield return new WaitForSeconds(15);
            m_Scope.SetActive(false);
            yield return new WaitForSeconds(1);
        }
    }
    
    IEnumerator BossPattern()
    {
        while (InfoMng.GetIns.BossHP >= 0)
        {
            yield return new WaitForSeconds(5);
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            m_Ani.SetTrigger("IsAttack");
            m_Sound.Sound_Boss(0, false, false).Play();
            yield return new WaitForSeconds(1);
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            BossPattern2();
            yield return new WaitForSeconds(10);
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            BossPattern1();
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }

        }
    }


    bool IsScopeHit = false;
    public void StartHit()
    {
        StartCoroutine("WireScopeHit");
    }
    IEnumerator WireScopeHit()
    {
        IsScopeHit = true;
        m_Sound.Sound_Boss(3,false,false).Play();

        Transform[] childList = m_Perent.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }

        m_Ani.gameObject.GetComponent<Animator>().enabled = false;
        m_GrapplingRope.enabled = false;
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.2f);
        m_CameraShake.ShakeStart();
        yield return new WaitForSeconds(4f);

        m_Ani.gameObject.GetComponent<Animator>().enabled = true;
        IsScopeHit = false;
    }
}
