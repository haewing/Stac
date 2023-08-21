using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaCrash : MonoBehaviour
{
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
                    Boss1att(3);
                   
                }
                if (Att == AttackMode.Att2)
                {
                    Boss1att(5);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att3)
                {
                    Boss1att(7);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Air)
                {
                    Boss1att(20);
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
                    Boss2att(2);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att2)
                {
                    Boss2att(3);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att3)
                {
                    Boss2att(2);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Air)
                {
                    Boss2att(20);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        if (collision.gameObject.tag == "TBoss")
        {
                Debug.Log("A");

            if (collision != null)
            {
                if (Att == AttackMode.Att1)
                {
                    TBossAtt(2);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att2)
                {
                    TBossAtt(3);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Att3)
                {
                    TBossAtt(2);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (Att == AttackMode.Air)
                {
                    TBossAtt(20);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
    void TBossAtt(int dmg)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        GameObject Ene = GameObject.Find("colliderInfo");
        InfoMng.GetIns.TBossHP -= dmg;
        if (InfoMng.GetIns.TBossHP <= 0) InfoMng.GetIns.TBossHP = 0;
        Ene.GetComponent<EnemyHP>().EnemyMark(150, InfoMng.GetIns.TBossHP, "보스");
    }
    void Boss2att(int dmg)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        GameObject Ene = GameObject.Find("colliderInfo");
        InfoMng.GetIns.BossHP2 -= dmg;
        if (InfoMng.GetIns.BossHP2 <= 0) InfoMng.GetIns.BossHP2 = 0;
        Ene.GetComponent<EnemyHP>().EnemyMark(300, InfoMng.GetIns.BossHP2, "보스");
    }
    void Boss1att(int dmg)
    {
        GameObject Ene = GameObject.Find("colliderInfo");
        InfoMng.GetIns.BossHP -= dmg;
        if (InfoMng.GetIns.BossHP <= 0) InfoMng.GetIns.BossHP = 0;
        Ene.GetComponent<EnemyHP>().EnemyMark(250, InfoMng.GetIns.BossHP, "보스");
    }
}
