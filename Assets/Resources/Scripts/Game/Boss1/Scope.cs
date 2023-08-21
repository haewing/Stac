using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scope : MonoBehaviour
{
    [SerializeField] CameraShake m_CameraShake;
    [SerializeField] GameObject m_Hit;
    [SerializeField] Boss m_Boss;
    [SerializeField] TBoss m_Boss1;
    [SerializeField] public Boss2 m_Boss2;
    [SerializeField] SoundMng m_Sound;

    Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            //if(scene.name == "Stage1")
            //{
            //    m_Boss1.StartHit();
            //    Hit();
            //}
            //if (scene.name == "Stage2")
            //{
            //    m_Boss.StartHit();
            //    Hit();
            //}
            //if (scene.name == "Test")
            //{
            //    m_Boss2.StartHit();
            //}
        }
        if (collision.gameObject.tag == "Kunai")
        {
            Hit();
            m_CameraShake.ShakeStart();

            if (scene.name == "Stage2")
            {
                InfoMng.GetIns.BossHP -= 4;
                if (InfoMng.GetIns.BossHP <= 0) InfoMng.GetIns.BossHP = 0;
            }
            if (scene.name == "Stage3")
            {
            }
        }
        if (collision.gameObject.tag == "Katana")
        {
            Hit();
            m_CameraShake.ShakeStart();

            if (scene.name == "Stage2")
            {
                InfoMng.GetIns.BossHP -= 18;
                if (InfoMng.GetIns.BossHP <= 0) InfoMng.GetIns.BossHP = 0;
            }
            if (scene.name == "Stage3")
            {
            }
        }
    }
    public void Hit()
    {
        m_Sound.Sound_Boss(4, false, false).Play();
        GameObject H = Instantiate(m_Hit,transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }


}
