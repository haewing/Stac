using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aura : MonoBehaviour
{
    [SerializeField] GameObject DmgTxt;
    public GameObject m_Par;
    public void Myretate(Vector3 ratete){
        transform.eulerAngles = ratete;
    }

    private void Start()
    {
        GameObject s = GameObject.Find("SoundMng");
        s.GetComponent<SoundMng>().Sound_Player(10, false, false).Play();
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector2(0,-50 * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Boss")
        {
            Vector3 CreatPos = Camera.main.WorldToScreenPoint(collision.transform.position);
            GameObject dxt = Instantiate(DmgTxt, CreatPos, Quaternion.identity, GameObject.Find("DmgParent").transform);
            dxt.GetComponent<Text>().text = (-10).ToString();


            GameObject Par = Instantiate(m_Par, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject Ene = GameObject.Find("colliderInfo");
            if (collision != null)
            {
                InfoMng.GetIns.BossHP -= 10;
                if (InfoMng.GetIns.BossHP <= 0) InfoMng.GetIns.BossHP = 0;
            }
        }
        if (collision.gameObject.tag == "Boss2")
        {
            Vector3 CreatPos = Camera.main.WorldToScreenPoint(collision.transform.position);
            GameObject dxt = Instantiate(DmgTxt, CreatPos, Quaternion.identity, GameObject.Find("DmgParent").transform);
            dxt.GetComponent<Text>().text = (-10).ToString();


            GameObject Par = Instantiate(m_Par, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject Ene = GameObject.Find("colliderInfo");
            if (collision != null)
            {
                InfoMng.GetIns.BossHP2 -= 10;
                if (InfoMng.GetIns.BossHP2 <= 0) InfoMng.GetIns.BossHP2 = 0;
            }
        }
        if (collision.gameObject.tag == "TBoss")
        {
            Vector3 CreatPos = Camera.main.WorldToScreenPoint(collision.transform.position);
            GameObject dxt = Instantiate(DmgTxt, CreatPos, Quaternion.identity, GameObject.Find("DmgParent").transform);
            dxt.GetComponent<Text>().text = (-10).ToString();


            GameObject Par = Instantiate(m_Par, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject Ene = GameObject.Find("colliderInfo");
            if (collision != null)
            {
                InfoMng.GetIns.TBossHP -= 10;
                if (InfoMng.GetIns.TBossHP <= 0) InfoMng.GetIns.TBossHP = 0;
            }
        }
    }
}
