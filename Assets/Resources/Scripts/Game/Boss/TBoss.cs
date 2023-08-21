using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBoss : MonoBehaviour
{
    [SerializeField] GrapplingRope m_GrapplingRope;
    [SerializeField] CameraShake m_CameraShake;
    [SerializeField] Transform[] m_WeaknessPos;
    [SerializeField] GameObject m_Scope;
    [SerializeField] SoundMng m_Sound;
    Animator ani;

    [HideInInspector]public float AttCol = 0;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        InitializeUP();
    }

    void Initialize()
    {
        ani = GetComponent<Animator>();
        Move();
        StartCoroutine(BossWeaknessCreate());
        StartCoroutine(BossDeath());

    }
    void InitializeUP()
    {
        speed = State == TBossState.Stern ? 0 : 15;
        if (IsScopeHit)
        {
            State = TBossState.Stern;
        }
        AttCol += Time.deltaTime;
    }


    enum TBossState
    {
        Move,
        Idel,
        Attack,
        Hit,
        Stern,
        Death,
    };
    TBossState State = TBossState.Idel;

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

    [SerializeField] BoxCollider2D m_AttBox1;
    [SerializeField] BoxCollider2D m_AttBox2;
    [SerializeField] BoxCollider2D m_AttRange;

    public void Att()
    {
        if(Random.Range(1,3) == 1)
        {
            Att1();
        }
        else
        {
            Att2();
        }
    }
    void AttDelay()
    {
        if(State == TBossState.Attack)
        {
            m_AttRange.enabled = false;
            Invoke("OnAttBox", 2);
        }
    }
    void OnAttBox()
    {
        m_AttRange.enabled = true;
    }

    void Att1()
    {
        StartCoroutine(Attack1());
    }
    IEnumerator Attack1()
    {
        ani.SetTrigger("IsAtt1");
        m_AttBox1.enabled = true;
        State = TBossState.Attack;
        yield return new WaitForSeconds(0.51f);
        m_AttBox1.enabled = false;
        if (State == TBossState.Stern) yield return new WaitForSeconds(0.1f);
        State = TBossState.Move;
    }
    void Att2()
    {
        StartCoroutine(Attack2());

    }
    IEnumerator Attack2()
    {
        ani.SetTrigger("IsAtt2");
        State = TBossState.Attack;
        m_AttBox1.enabled = true;
        yield return new WaitForSeconds(0.51f);
        m_AttBox1.enabled = false;
        if (State == TBossState.Stern) yield return new WaitForSeconds(0.1f);
        State = TBossState.Move;
    }
    void Move()
    {
        StartCoroutine(MoveCheak());
        StartCoroutine(MovePattern());

    }
    float MoveTime = 0;
    IEnumerator MoveCheak()
    {
        while (State != TBossState.Death)
        {
            if(State == TBossState.Attack || State == TBossState.Hit || State == TBossState.Stern) { yield return new WaitForSeconds(0.1f); }
            State = TBossState.Move;
            MoveDir();
            yield return new WaitForSeconds(1.0f);
            yield return new WaitForSeconds(1.0f);
            if(State == TBossState.Attack || State == TBossState.Hit || State == TBossState.Stern) { yield return new WaitForSeconds(0.1f); }
            State = TBossState.Idel;
            yield return new WaitForSeconds(1.0f);


        }
    }
    void MoveDir()
    {
        if (transform.position.x > GameObject.Find("Player").transform.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
    }
    int dir = 1;
    int speed = 15;
    IEnumerator MovePattern()
    {
        while(gameObject != null)
        {
            if(State == TBossState.Death) { break; }
            if (State == TBossState.Move)
            {

                if (transform.position.x > 370 && transform.position.x < 455)
                {
                    ani.SetBool("IsRun", true);

                    transform.localEulerAngles = dir == 1 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
                    gameObject.transform.Translate(new Vector2(speed * dir, 0) * Time.deltaTime,Space.World);
                }
                else
                {
                    ani.SetBool("IsRun", false);
                    dir = transform.position.x > 413 ? -1 : 1;
                    gameObject.transform.Translate(new Vector2(speed * dir, 0) * Time.deltaTime,Space.World);
                   
                    yield return new WaitForSeconds(1);
                }
            }
            if(State == TBossState.Idel || State == TBossState.Attack )
            {
                
                ani.SetBool("IsRun", false);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    IEnumerator BossDeath()
    {
        while (gameObject != null)
        {
            if (InfoMng.GetIns.TBossHP <= 0)
            {
                State = TBossState.Death;
                GameMng.GetIns.CameraMode = 3;
                yield return new WaitForSeconds(1.0f);
                ani.gameObject.GetComponent<Animator>().enabled = true;
                ani.SetTrigger("IsDeath");
                yield return new WaitForSeconds(2.0f);
                GameMng.GetIns.BossClear = true;
                Destroy(gameObject);
                GameMng.GetIns.CameraMode = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void StartHit()
    {
        StartCoroutine("WireScopeHit");
    }
    bool IsScopeHit = false;
    IEnumerator WireScopeHit()
    {
        int a = (int)State;
        State = TBossState.Stern;
        IsScopeHit = true;
        m_Sound.Sound_Boss(3, false, false).Play();



        ani.gameObject.GetComponent<Animator>().enabled = false;
        m_GrapplingRope.enabled = false;
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.2f);
        m_CameraShake.ShakeStart();
        yield return new WaitForSeconds(4f);

        ani.gameObject.GetComponent<Animator>().enabled = true;
        IsScopeHit = false;
        State = (TBossState)a;
    }
}
