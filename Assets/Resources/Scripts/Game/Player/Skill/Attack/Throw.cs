using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throw : MonoBehaviour
{
    public GameObject m_Par;

    [SerializeField] GameObject DmgTxt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Drone")
        {

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Boss")
        {
            DmgTxtfunction(2, collision);

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
            GameObject.Find("colliderInfo").GetComponent<EnemyHP>().yellowHP();
        }
        if (collision.gameObject.tag == "Boss2")
        {
            DmgTxtfunction(2, collision);

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
            GameObject.Find("colliderInfo").GetComponent<EnemyHP>().yellowHP();
        }
        if (collision.gameObject.tag == "TBoss")
        {
            DmgTxtfunction(2, collision);

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
            GameObject.Find("colliderInfo").GetComponent<EnemyHP>().yellowHP();
        }
    }
    void DmgTxtfunction(int dmg, Collider2D coll)
    {
        Vector3 CreatPos = Camera.main.WorldToScreenPoint(coll.transform.position);

        GameObject dxt = Instantiate(DmgTxt, CreatPos, Quaternion.identity, GameObject.Find("DmgParent").transform);
        dxt.GetComponent<Text>().text = (-dmg).ToString();
    }


}
