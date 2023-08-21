using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    [SerializeField] GrapplingRope m_GrapplingRope;
    [SerializeField] CameraShake m_CameraShake;
    [SerializeField] Transform[] m_WeaknessPos;
    [SerializeField] GameObject m_Scope;
    [SerializeField] Scope m_ScopeScript;
    [SerializeField] SoundMng m_Sound;

    [Header("PatternItem")]
    [SerializeField] GameObject m_FireBall;
    [SerializeField] GameObject m_FireChild;
    [SerializeField] GameObject m_Laser;

    [SerializeField] GameObject m_BossBullet;
    [SerializeField] GameObject m_BossFloor;
    [SerializeField] Transform m_Perent;

    [SerializeField] Transform[] m_TP_Pos;

    [SerializeField] GameObject m_Energy;

    [HideInInspector] public Animator m_Ani;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    enum BossState
    {
        Stern,
        Nomal,
        Move,
        Outburst,
        Death,
    };
    BossState State = BossState.Nomal;
    // Update is called once per frame
    bool boolDeath = false;
    int OutburstCount = 0;
    void Update()
    {
        InitializeUp();
    }

    public void Initialize()
    {

        m_Ani = gameObject.GetComponent<Animator>();
        StartCoroutine(BossWeaknessCreate());
        StartCoroutine("Pattern");
        StartCoroutine(Rusid());
        StartCoroutine(Out());
        StartCoroutine(LaserEff());
        StartCoroutine(BossDeath());
    }
    public void InitializeUp()
    {
        if (InfoMng.GetIns.BossHP2 <= 0 && !boolDeath)
        {
            boolDeath = true;
            State = BossState.Outburst;

        }
        if(OutburstCount >= 5)
        {
            State = BossState.Death;
        }
        
        Outburst();
        BossMove();
        XYLaser();

    }

    bool IsOutBurst = false;
    
    [SerializeField] GameObject m_LX;
    [SerializeField] GameObject m_LY;


    [SerializeField] GameObject m_Laser1;
    [SerializeField] GameObject m_Laser2;

    [SerializeField] GameObject m_GuideLaser1;
    [SerializeField] GameObject m_GuideLaser2;


    IEnumerator BossDeath()
    {
        while (gameObject != null)
        {
            if (State == BossState.Death)
            {
                GameMng.GetIns.CameraMode = 3;
                yield return new WaitForSeconds(1.0f);
                m_Ani.gameObject.GetComponent<Animator>().enabled = true;
                m_Ani.SetTrigger("IsDeath");
                yield return new WaitForSeconds(2.0f);
                Destroy(gameObject);
                GameMng.GetIns.CameraMode = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void XYLaser()
    {
        Transform p = GameObject.Find("Player").transform;

        if (L && p.position.x >= -35 && p.position.x <= 53 && p.position.y >= -14 && p.position.y <= 68)
        {
            m_LX.transform.position = new Vector2(p.position.x, 0);
            m_LY.transform.position = new Vector2(0, p.position.y);
        }
    }
    bool L = true;
    IEnumerator LaserEff()
    {
        while(gameObject != null)
        {
            Transform p = GameObject.Find("Player").transform;
            if (p.position.x >= -35 && p.position.x <= 53 && p.position.y >= -14 && p.position.y <= 68)
            {
                yield return new WaitForSeconds(15);
                m_GuideLaser1.SetActive(true);
                m_GuideLaser2.SetActive(true);

                yield return new WaitForSeconds(1);
                for (int i = 0; i < 3; i++)
                {
                    L = true;
                    m_Laser1.SetActive(false);
                    m_Laser2.SetActive(false);

                    yield return new WaitForSeconds(1);
                    L = false;
                    yield return new WaitForSeconds(0.35f);
                    m_Laser1.SetActive(true);
                    m_Laser2.SetActive(true);
                    yield return new WaitForSeconds(1);
                    L = true;

                }
                m_Laser1.SetActive(false);
                m_Laser2.SetActive(false);
                L = true;

                m_GuideLaser1.SetActive(false);
                m_GuideLaser2.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);
        }
    } 
    public void IsOut()
    {
        IsOutBurst = true;
    }
    IEnumerator Out()
    {
        while(State != BossState.Outburst)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        StopCoroutine("Pattern");
        m_Ani.SetTrigger("IsOut");
    }
    
    void Outburst()
    {

        if (State == BossState.Outburst && IsOutBurst && !IsScopeHit)
        {
            m_Energy.SetActive(true);

            GameObject p = GameObject.Find("Player");
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;

            transform.position = Vector2.MoveTowards(transform.position, p.transform.position, 20f * Time.deltaTime);

            gameObject.layer = 10;
        }
        else if(IsScopeHit)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

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
            GameObject Laser = Instantiate(m_Laser, new Vector2(P.transform.position.x, P.transform.position.y - 3), Quaternion.identity);
            float z = Random.Range(0, 360);
            Laser.transform.eulerAngles = new Vector3(0, 0, z);
            Destroy(Laser, 1);
            yield return new WaitForSeconds(1f - (i / 10));
        }
    }
    public void Heal()
    {
        InfoMng.GetIns.BossHP2 += 30;
        if(InfoMng.GetIns.BossHP2 <= 300) InfoMng.GetIns.BossHP2 = 300;
    }
    public void Pattern1()
    {
        StartCoroutine(Dot());
        //StartCoroutine(Dot1());
    }
    //IEnumerator Dot1()
    //{
    //for (int i = 0; i < 4; i++)
    //{
    //    if (IsScopeHit) { break; }
    //    for (int j = 0; j < 45; j++)
    //    {
    //        if (IsScopeHit) { break; }
    //        Vector2 p = new Vector2(transform.position.x, transform.position.y-5);
    //        GameObject Bullet = Instantiate(m_BossBullet, p, Quaternion.identity, m_Perent);
    //        Bullet.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    //        Bullet.GetComponent<BossBullet>().BulletInit((j * 8), 10);

    //    }
    //    yield return new WaitForSeconds(1.3f);
    //}

    //for (int j = 0; j <= 45; j++)
    //{
    //    if (IsScopeHit) { break; }
    //    Vector2 p = new Vector2(transform.position.x, transform.position.y - 5);
    //    GameObject Bullet = Instantiate(m_BossBullet, p, Quaternion.identity, m_Perent);
    //    Bullet.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    //    Bullet.GetComponent<BossBullet>().BulletInit((j * -8), 10);
    //    yield return new WaitForSeconds(0.02f);
    //}


    //}
    public Transform Pattern2_PlayerPos;
    IEnumerator Dot()
    {


        //for (int i = 0; i < 3; i++)
        //{
        //    if (IsScopeHit) { break; }
        //    for (int j = 0; j <= 45; j++)
        //    {
        //        if (IsScopeHit) { break; }
        //        Vector2 p = new Vector2(transform.position.x, transform.position.y - 5);
        //        GameObject Bullet = Instantiate(m_BossBullet, p, Quaternion.identity, m_Perent);
        //        Bullet.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        //        Bullet.GetComponent<BossBullet>().BulletInit((j * 8), 10);
        //        yield return new WaitForSeconds(0.02f);
                  
        //    }
        //}


        List<GameObject> b = new List<GameObject>();
        for (int j = 0; j < 45; j++)
        {
            if (IsScopeHit) { break; }
            GameObject Bullet = Instantiate(m_BossBullet, new Vector2(transform.position.x, transform.position.y - 4), Quaternion.identity, m_Perent);
            b.Add(Bullet);
            Bullet.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            Bullet.GetComponent<BossBullet>().BulletInit((j * 8), 10);
        }

        yield return new WaitForSeconds(3);

        for (int j = 0; j < b.Count; j++)
        {
            if (b[j] != null)
                b[j].GetComponent<BossBullet>().speed =0;
        }
        yield return new WaitForSeconds(1);
        Pattern2_PlayerPos = GameObject.Find("Player").transform;
        for (int i = 0; i < b.Count; i++)
        {
            if (b[i] != null)
            {

                b[i].GetComponent<BossBullet>().PT2 = true;
            }
        }
        //b.Clear();
    }

    public void Pattern2()
    {
        StartCoroutine(FireBallShoot());
    }
    IEnumerator FireBallShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            if (IsScopeHit) { break; }
            GameObject f = Instantiate(m_FireBall, transform);
            f.GetComponent<FireBall>().PlayerLook();

            yield return new WaitForSeconds(0.5f);
        }

    }
    Vector2 MovePos;
    float ScaleX = 20;
    void BossMove()
    {

        if(State == BossState.Move)
        {
            ScaleX = Mathf.Lerp(ScaleX, 0, 0.5f);
            if (ScaleX <= 0.2f)
            {
                transform.position = new Vector2(MovePos.x,MovePos.y +13);
                State = BossState.Nomal;
            }
        }
        else
        {

            ScaleX = Mathf.Lerp(ScaleX, 20, 0.2f);
        }
        transform.localScale = new Vector3(ScaleX, 20, 20);

    }
    void FloorCreate()
    {
        StartCoroutine(Boader());
    }
    IEnumerator Boader()
    {
        
        GameObject p = GameObject.Find("Player");
        MovePos = p.transform.position;

        GameObject b = Instantiate(m_BossFloor, new Vector2(MovePos.x,MovePos.y + 4),Quaternion.identity);
        b.GetComponent<Rigidbody2D>().gravityScale = 1;

        yield return new WaitForSeconds(2);

        b.GetComponent<Rigidbody2D>().gravityScale = 0;
        b.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        MovePos = b.transform.position;
        Destroy(b, 33);

    }
    public void Death()
    {
        m_Ani.SetTrigger("IsDeath");

    }
    int i = 0;
    IEnumerator Pattern()
    {
        while(gameObject!= null && InfoMng.GetIns.BossHP2 > 0)
        {
            yield return new WaitForSeconds(3f);
            m_Ani.SetTrigger("IsCast2");
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            yield return new WaitForSeconds(1.5f);
            Pattern1();
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            yield return new WaitForSeconds(10);
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            m_Ani.SetTrigger("IsCast1");
            yield return new WaitForSeconds(1.5f);
            Pattern2();
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            yield return new WaitForSeconds(10);
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            FloorCreate();
            yield return new WaitForSeconds(2);
            while (IsScopeHit) { yield return new WaitForSeconds(0.1f); }
            State = BossState.Move;

            yield return new WaitForSeconds(5);
           
            i++;
            if(i >= 3)
            {
                m_Ani.SetTrigger("IsHeal");
                yield return new WaitForSeconds(0.5f);
                Heal();
                i = 0;
            }
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
    public void Hit(float Dmg)
    {
        m_Ani.SetTrigger("IsHit");
        InfoMng.GetIns.BossHP2 -= Dmg;
    }

    bool IsScopeHit = false;
    public void StartHit()
    {
        StartCoroutine("WireScopeHit");
    }
    IEnumerator WireScopeHit()
    {
        int a = (int)State;
        if (State == BossState.Outburst)
        {
            OutburstCount++;
            
        }
        State = BossState.Stern;
        IsScopeHit = true;
        m_Sound.Sound_Boss(3, false, false).Play();

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
        State = (BossState)a ;
        if (InfoMng.GetIns.BossHP2 <= 0) State = BossState.Outburst;
        Debug.Log(OutburstCount);
    }
}
