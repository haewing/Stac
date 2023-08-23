using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject m_Par;



    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Boss")
        {

            GameObject s = GameObject.Find("SoundMng");
            s.GetComponent<SoundMng>().Sound_Player(8, false, false).Play();

            GameObject Par = Instantiate(m_Par, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject Ene = GameObject.Find("colliderInfo");
            if (collision != null)
            {
                InfoMng.GetIns.BossHP -= 2;
            if (InfoMng.GetIns.BossHP <= 0) InfoMng.GetIns.BossHP = 0;
            }
        }
        if (collision.gameObject.tag == "Boss2")
        {

            GameObject s = GameObject.Find("SoundMng");
            s.GetComponent<SoundMng>().Sound_Player(8, false, false).Play();

            GameObject Par = Instantiate(m_Par, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject Ene = GameObject.Find("colliderInfo");
            if (collision != null)
            {
                InfoMng.GetIns.BossHP2 -= 2;
            if (InfoMng.GetIns.BossHP2 <= 0) InfoMng.GetIns.BossHP2 = 0;
            }
        }
        if (collision.gameObject.tag == "TBoss")
        {

            GameObject s = GameObject.Find("SoundMng");
            s.GetComponent<SoundMng>().Sound_Player(8, false, false).Play();

            GameObject Par = Instantiate(m_Par, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject Ene = GameObject.Find("colliderInfo");
            if (collision != null)
            {
                InfoMng.GetIns.TBossHP -= 2;
                if (InfoMng.GetIns.TBossHP <= 0) InfoMng.GetIns.TBossHP = 0;
            }
        }
    }

}
