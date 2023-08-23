using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KatanaCrash : MonoBehaviour
{
    [SerializeField] GameObject DmgTxt;
    [SerializeField] Transform parent;
    public enum AttackMode
    {
        No,
        Att1,
        Att2,
        Att3,
        Air
    };
    [HideInInspector]public AttackMode Att = AttackMode.Att1;
    public void AttacMode()
    {

    }
    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Boss")
        {
            if (collision != null)
            {
                if (Att == AttackMode.Att1)
                {
                    Boss1att(3, collision);
                   
                }
                if (Att == AttackMode.Att2)
                {
                    Boss1att(5, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att3)
                {
                    Boss1att(7, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Air)
                {
                    Boss1att(20, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        if (collision.gameObject.tag == "Boss2")
        {
            if (collision != null)
            {
                if (Att == AttackMode.Att1)
                {
                    Boss2att(2, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att2)
                {
                    Boss2att(3, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att3)
                {
                    Boss2att(2, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Air)
                {
                    Boss2att(20, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        if (collision.gameObject.tag == "TBoss")
        {

            if (collision != null)
            {
                if (Att == AttackMode.Att1)
                {
                    TBossAtt(2,collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att2)
                {
                    TBossAtt(3, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att3)
                {
                    TBossAtt(2, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Air)
                {
                    TBossAtt(20, collision);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
    void TBossAtt(int dmg, Collider2D coll)
    {
        Vector3 CreatPos = Camera.main.WorldToScreenPoint(coll.transform.position);

        GameObject dxt =  Instantiate(DmgTxt, CreatPos, Quaternion.identity,parent);
        dxt.GetComponent<Text>().text = (-dmg).ToString();
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        GameObject Ene = GameObject.Find("colliderInfo");
        InfoMng.GetIns.TBossHP -= dmg;
        if (InfoMng.GetIns.TBossHP <= 0) InfoMng.GetIns.TBossHP = 0;
    }
    void Boss2att(int dmg, Collider2D coll)
    {
        Vector3 CreatPos = Camera.main.WorldToScreenPoint(coll.transform.position);
        GameObject dxt = Instantiate(DmgTxt, CreatPos, Quaternion.identity, parent);
        dxt.GetComponent<Text>().text = (-dmg).ToString();
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        GameObject Ene = GameObject.Find("colliderInfo");
        InfoMng.GetIns.BossHP2 -= dmg;
        if (InfoMng.GetIns.BossHP2 <= 0) InfoMng.GetIns.BossHP2 = 0;
    }
    void Boss1att(int dmg, Collider2D coll)
    {
        Vector3 CreatPos = Camera.main.WorldToScreenPoint(coll.transform.position);
        GameObject dxt = Instantiate(DmgTxt, CreatPos, Quaternion.identity, parent);
        dxt.GetComponent<Text>().text = (-dmg).ToString();
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        GameObject Ene = GameObject.Find("colliderInfo");
        InfoMng.GetIns.BossHP -= dmg;
        if (InfoMng.GetIns.BossHP <= 0) InfoMng.GetIns.BossHP = 0;
    }
}
