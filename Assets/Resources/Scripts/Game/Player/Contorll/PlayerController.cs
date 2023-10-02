using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GrapplingRope m_GrapplingRope;
    [Header("Camera")]
    public CameraShake m_CameraShake;
    [Header("Sound")]
    public SoundMng m_Sound;
    [Header("PlayerInfo")]
    public Rigidbody2D rigid;
    [Range(0, 100)] public int JumpPower;
    [Range(0, 100)] public float speed;
    public Text m_WeaponState;
    [SerializeField] Transform m_View;

    [Header("Attack")]
    public GameObject BulletEffect;
    public GameObject Bullet;
    public GrapplingGun Grapp;
    public GameObject m_SwordAura;

    [Header("Anime")]
    public Animator ani;

    bool IsJump = true;
    float ViewCheck = 0;
    bool look = false;
    int value;

    [HideInInspector] public bool Tu_IsAxis = false;
    [HideInInspector] public bool Tu_Att = false;
    [HideInInspector] public bool Tu_IsJump = false;
    [HideInInspector] public bool Tu_Melee = false;
    [HideInInspector] public bool Tu_Far = false;
    [HideInInspector] public bool Tu_IsSkill = false;

    [HideInInspector]
    public float hor;
    public void Update()
    {


    }
    void Start()
    {
    }
    enum PlayerState
    {
        Nomal,
        Death,
    }
    PlayerState State = PlayerState.Nomal;
    public void InitializeUpdate()
    {
        if (State == PlayerState.Nomal) {
            PlayerMove();//이동
            PlayerJump();//점프
            PlayerControll();//그래플
            PlayerView();// 시야
            PlayerAttack();//공격
            PlayerSkill();//스킬
            Viewguide();
            PlayerDeathCheck();
            //PlayerSpeedLimit();
        }
        else
        {
            PlayerDeath();
        }
    }

    [SerializeField] GameObject DeathPanel;
    [SerializeField] GameObject DeathTime;
    [SerializeField] GameObject DeathBtn;
    bool test = false;
    Vector3 Movepos;
    float ExitTest = 0;
    bool DeathCorCheack = false;    
    void PlayerDeath()
    {
        InfoMng.GetIns.PlayerHP = 0;
        if (!DeathCorCheack)
        StartCoroutine(DeathCor());
    }
    IEnumerator DeathCor()
    {
        ani.SetTrigger("IsDead");
        DeathCorCheack = true;

        yield return new WaitForSeconds(0.4f);
        DeathPanel.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        DeathTime.SetActive(true);
        float curTime = 0;
        while (curTime != GameMng.GetIns.PlayTime -0.5f)
        {
            Debug.Log(curTime);
            DeathTime.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(curTime / 60), Mathf.Round(curTime % 60));
            curTime = Mathf.Lerp(curTime, GameMng.GetIns.PlayTime, 20f  * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
            if(curTime > GameMng.GetIns.PlayTime - 0.4f)
            {
                break;
            }

            
        }
        yield return new WaitForSeconds(0.5f);
        DeathBtn.SetActive(true);



    }
    void PlayerDeathCheck()
    {
        if(InfoMng.GetIns.PlayerHP <= 0)
        {
            State = PlayerState.Death;
        }
    }
    public void PlayerSpeedLimit()
    {
        if (Mathf.Abs(rigid.velocity.x) > 80)
        {
            rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * 5, rigid.velocity.y);
        }
        if (Mathf.Abs(rigid.velocity.y) > 80)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Sign(rigid.velocity.y) * 5);
        }
    }
    public void Viewguide()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(m_View.transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        m_View.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
       
    }
    public void PlayerSkill()
    {
        value = look ? -1 : 1;
        if (test)
        {
            rigid.velocity = Vector2.zero;
            transform.position = Vector2.Lerp(transform.position, Movepos, Time.deltaTime * 0.1f);

            ExitTest += Time.deltaTime;
            if (ExitTest > 0.1f) test = false;


        }


        GameMng.GetIns.SkillCoolTime -= Time.deltaTime;
        if (GameMng.GetIns.SkillCoolTime < 0 && Input.GetKeyDown(KeyCode.E))
        {
            Tu_IsSkill = true;
            if (GameMng.GetIns.PlayerAttackMode == 0)
            {
                Invoke("OffAnime", 0.4f);
                GameMng.GetIns.KnifeCoolTime = 0.4f;
                


                GameObject Aura = Instantiate(m_SwordAura, transform.position, Quaternion.identity);
                Aura.GetComponent<Aura>().Myretate(new Vector3(0, 0, 90 * value));
                Destroy(Aura, 10);
            }

            if (GameMng.GetIns.PlayerAttackMode == 1)
            {
                rigid.velocity = Vector2.zero;

                value = look ? -1 : 1;
                Movepos = new Vector2(transform.position.x - (1000 * value), transform.position.y + 300);
                test = true;
                ExitTest = 0;
                Vector3 dir = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

                for (int i = 0; i < 5; i++)
                {
                    GameObject b = Instantiate(Bullet, dir, Quaternion.identity);
                    int y = look ? 180 : 0;
                    b.GetComponent<Bullet>().MyQuaternion(new Vector3(0, y, (i * 15) + 150));
                    Destroy(b, 10f);
                }
            }
            GameMng.GetIns.SkillCoolTime = 5;
        }
        else if (GameMng.GetIns.SkillCoolTime > 0 && Input.GetKeyDown(KeyCode.E))
        {
            ValueInit();
            m_WeaponState.text = "스킬 쿨타임";
            TextUpfloat += Time.deltaTime * 0.6f;
            TextAlpha -= Time.deltaTime;


            m_WeaponState.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f + TextUpfloat, 0));
            m_WeaponState.color = new Color(1, 1, 1, TextAlpha);
        }

    }
    public GameObject AttObj;
    public void AttackModeNo()
    {
        AttObj.GetComponent<BoxCollider2D>().enabled = false;
        AttObj.GetComponent<KatanaCrash>().Att = KatanaCrash.AttackMode.No;
    }
    public void AttackMode1()
    {
        gameObject.transform.Translate(new Vector2(0.00001f, 0));
        AttObj.GetComponent<BoxCollider2D>().enabled = true;
        
        AttObj.GetComponent<KatanaCrash>().Att = KatanaCrash.AttackMode.Att1;
        
    }
    public void AttackMode2()
    {
        gameObject.transform.Translate(new Vector2(0.00001f, 0));
        AttObj.GetComponent<BoxCollider2D>().enabled = true;
        AttObj.GetComponent<KatanaCrash>().Att = KatanaCrash.AttackMode.Att2;
    }
    public void AttackMode3()
    {
        gameObject.transform.Translate(new Vector2(0.00001f, 0));
        AttObj.GetComponent<BoxCollider2D>().enabled = true;
        AttObj.GetComponent<KatanaCrash>().Att = KatanaCrash.AttackMode.Att3;
    }
    public void AttackModeAir()
    {
        AttObj.GetComponent<BoxCollider2D>().enabled = true;
        AttObj.GetComponent<KatanaCrash>().Att = KatanaCrash.AttackMode.Air;
    }


    public void PlayerControll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) Grapp.autoConfigureDistance = false;
        if (Input.GetKeyUp(KeyCode.LeftControl)) Grapp.autoConfigureDistance = true;



    }
    
    public void PlayerView()
    {

        if (!IsJump)
        {
            ViewCheck += Time.deltaTime;
            if (ViewCheck >= 1 && Input.GetKey(KeyCode.Space))
            {
                Camera.main.orthographicSize += 10 * Time.deltaTime;
                if (Camera.main.orthographicSize >= 35)
                {
                    Camera.main.orthographicSize = 35;
                }
            }
            else
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 20.0f, Time.deltaTime * 3);
            }
        }
        else
        {
            ViewCheck = 0;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 20.0f, Time.deltaTime * 3);

        }

    }

    float TextUpfloat = 0;
    float TextAlpha = 1;

    void ValueInit()
    {
        TextUpfloat = 0;
        TextAlpha = 1;
    }
    float Mod2CoolTimeCheckf;
    public void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Tu_Melee = true;
            GameMng.GetIns.PlayerAttackMode = 0;
            m_WeaponState.text = "근거리";
            m_Sound.Sound_Player(2, false, false).Play();
            ValueInit();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Tu_Far = true;
            GameMng.GetIns.PlayerAttackMode = 1;
            m_WeaponState.text = "원거리";
            m_Sound.Sound_Player(2, false, false).Play();
            ValueInit();
        }

        TextUpfloat += Time.deltaTime * 0.6f;
        TextAlpha -= Time.deltaTime;


        m_WeaponState.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f + TextUpfloat, 0));
        m_WeaponState.color = new Color(1, 1, 1, TextAlpha);


        if (IsJump)
        {
            GameMng.GetIns.KnifeCoolTime -= Time.deltaTime;
            //if (Input.GetMouseButtonDown(0) && GameMng.GetIns.PlayerAttackMode == 0 && GameMng.GetIns.KnifeCoolTime < 0)
            //{     
            //    if (!AttackCor)
            //    {
            //        AttackCor = true;
            //        StartCoroutine(AttackAni());

            //    }
            //}
            if (Input.GetMouseButton(0) && GameMng.GetIns.PlayerAttackMode == 0 && GameMng.GetIns.KnifeCoolTime < 0)
            {
                AttackTime += 0.2f;
                if (!AttackCor)
                {
                    Tu_Att = true;
                    AttackCor = true;
                    StartCoroutine(AttackAni());

                }
            }



            GameMng.GetIns.KunaiCoolTime -= Time.deltaTime;
            if (GameMng.GetIns.KunaiCoolTime < 0)
            {
                //원거리
                if (Input.GetMouseButton(0) && GameMng.GetIns.PlayerAttackMode == 1)
                {
                    Tu_Att = true;
                    m_Sound.Sound_Player(1, false, false).Play();
                    Vector3 dir = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z);
                    //GameObject be = Instantiate(BulletEffect, dir, Quaternion.identity);
                    //Destroy(be, 0.5f);

                    ani.SetTrigger("IsCast");

                    GameObject b = Instantiate(Bullet, dir, Quaternion.identity);
                    b.GetComponent<Bullet>().MyQuat();
                    Destroy(b, 5f);
                    GameMng.GetIns.KunaiCoolTime = 0.3f;
                }
            }
        }
        else
        {
            GameMng.GetIns.KnifeCoolTime -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && GameMng.GetIns.PlayerAttackMode == 0 && GameMng.GetIns.KnifeCoolTime < 0)
            {

                ani.SetTrigger("AirAttack1");
                GameMng.GetIns.KnifeCoolTime = 0.5f;

            }


            GameMng.GetIns.KunaiCoolTime -= Time.deltaTime;
            if (GameMng.GetIns.KunaiCoolTime < 0)
            {
                //원거리
                if (Input.GetMouseButton(0) && GameMng.GetIns.PlayerAttackMode == 1)
                {
                    m_Sound.Sound_Player(1, false, false).Play();
                    Vector3 dir = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z);
                    //GameObject be = Instantiate(BulletEffect, dir, Quaternion.identity);
                    //Destroy(be, 0.5f);

                    ani.SetTrigger("AirAttack2");

                    GameObject b = Instantiate(Bullet, dir, Quaternion.identity);
                    b.GetComponent<Bullet>().MyQuat();
                    Destroy(b, 5f);
                    GameMng.GetIns.KunaiCoolTime = 0.3f;
                }
            }
        }
    }
    bool AttackCor = false;
    bool AttackSpeed = false;
    float AttackTime = 0.1f;
    IEnumerator AttackAni()
    {
        
        ani.SetBool("IsAttack", true);
        yield return new WaitForSeconds(AttackTime);
        ani.SetBool("IsAttack", false);
        AttackTime = 0.1f;
        AttackCor = false;
    }
    public void PlayerSpeedZero()
    {
        AttackSpeed = true;
    }
    public void PlayerSpeed()
    {
        AttackSpeed = false;
    }

    public void PlayerMove()
    {
        if (AttackSpeed)
        {
            speed = 0;
        }
        else
        {
            speed = 16;
        }

        hor = Input.GetAxis("Horizontal");


        if (GameMng.GetIns.PlayerGrapplingHitCheck)
        {
            rigid.AddForce(new Vector2((hor) * 6, 0));
            speed = 0;
        }
        else
        {

                speed = Mathf.Lerp(speed, 20.0f, Time.deltaTime * 2);

                gameObject.transform.Translate(new Vector3(hor * speed * Time.deltaTime, 0), Space.World);

            
        }
        if (hor > 0) { gameObject.transform.eulerAngles = new Vector3(0, 0, 0); look = false; }
        if (hor < 0) { gameObject.transform.eulerAngles = new Vector3(0, 180, 0); look = true; }


        if (IsJump)
        {
            if (hor != 0)
            {
                ani.SetBool("IsRun", true);
                Tu_IsAxis = true;
            }
            else ani.SetBool("IsRun", false);
        }
        if (!IsJump)
        {
            ani.SetBool("IsRun", false);
            ani.SetBool("IsFly", true);
        }

    }
    public void Tumbling()
    {

        m_Sound.Sound_Player(3, false, false).Play();
        m_Sound.Sound_Player(6, false, false).Play();
        ani.SetTrigger("IsJump");
    }

    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsJump)
        {
            Tu_IsJump = true;
            m_Sound.Sound_Player(0, false, false).Play();
            rigid.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
            //GameMng.GetIns.PlayerGrapplingHitCheck = false;
            IsJump = false;
        }

    }
    public void AttSound1()
    {
        m_Sound.Sound_Player(10, false, false).Play();
    }

    public void AttSound2()
    {
        m_Sound.Sound_Player(11, false, false).Play();

    }
    public void AttSound3()
    {

        m_Sound.Sound_Player(12, false, false).Play();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            IsJump = true;
            ani.SetBool("IsFly", false);
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            IsJump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Floor")
        {
            rigid.velocity = Vector2.Lerp(rigid.velocity,Vector2.zero,0.5f);
        }
        if (test)
        {
            test = false;
            transform.position = new Vector2(transform.position.x + (1 * value),transform.position.y);
        }



    }
    
    
    [SerializeField] GameObject MiniCamera;
    float EnergyTime = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        EnergyTime += Time.deltaTime;
        if (collision.gameObject.name == "Energy" && EnergyTime >= 0.2f)
        {
            Hit(1);
            EnergyTime = 0;
        }

        if (collision.gameObject.name == "AttDetect" && GameObject.Find("Boss").GetComponent<TBoss>().AttCol > 2)
        {
            GameObject.Find("Boss").GetComponent<TBoss>().AttCol = 0;
            GameObject.Find("Boss").GetComponent<TBoss>().Att();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "fall")
        {
            m_GrapplingRope.enabled = false;
            gameObject.transform.position = new Vector3(-1, -11, 0);

        }

        if(collision.gameObject.tag == "Zoom")
        {
            rigid.velocity = Vector2.zero;
            if(collision.gameObject.name == "Zoom")
            {
                GameMng.GetIns.CameraMode = 1;
            }
            if (collision.gameObject.name == "Zoom (1)" && GameObject.Find("Drone") != null)
            {
                MiniCamera.SetActive(true);
                GameMng.GetIns.CameraMode = 2;
            }
            Invoke("CameraInit", 1f);
        }
        if(collision.gameObject.name == "IsAtt1Range" || collision.gameObject.name == "IsAtt2Range")
        {
            Hit(5);
        }
        if(collision.gameObject.tag == "Laser")
        {
            Hit(3);
        }
        if (collision.gameObject.tag == "DroneBullet")
        {
            Hit(5);

        }
        if(collision.gameObject.tag == "BossBullet")
        {
            Hit(3);
        }
        if (collision.gameObject.tag == "FireChild")
        {
            Hit(3);
        }
        if (collision.gameObject.tag == "Fire")
        {
            Hit(5);
        }

    }
    void CameraInit()
    {
        GameMng.GetIns.CameraMode = 0;
    }
    void Hit(int Dmg)
    {
        m_CameraShake.ShakeStart();
        InfoMng.GetIns.PlayerHP -= Dmg;
        m_Sound.Sound_Player(7, false, false).Play();
    }
}