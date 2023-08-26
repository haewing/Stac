using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] public GameObject SetHP;
    [SerializeField] private Image m_HPBack;
    [SerializeField] private Image m_HPY;
    [SerializeField] public Image m_HP;
    //[SerializeField] private Text m_HPText;
    //[SerializeField] private Text _Name;


  
   
    public void Initialize()
    {

    }
    public void InitializeUp()
    {
        Vector2 ThisPos = Camera.main.WorldToScreenPoint(GameObject.Find("Boss").transform.position);
        Vector2 CorrectionPos = new Vector2(ThisPos.x, ThisPos.y + 120);
        m_HP.transform.position = CorrectionPos;
        m_HPY.transform.position = CorrectionPos;
        m_HPBack.transform.position = CorrectionPos;



        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage1":
                m_HP.fillAmount = InfoMng.GetIns.TBossHP / 150;
                break;
            case "Stage2":
                m_HP.fillAmount = InfoMng.GetIns.BossHP / 250;
                break;
            case "Stage3":
                m_HP.fillAmount = InfoMng.GetIns.BossHP2 / 300;
                break;
        }

        if(y) m_HPY.fillAmount = Mathf.Lerp(m_HPY.fillAmount, m_HP.fillAmount, 10f * Time.deltaTime);
    }
    bool y = false; 
    public void yellowHP()
    {
        StartCoroutine(WaitHP());
    }
    IEnumerator WaitHP()
    {
        y = false;
        yield return new WaitForSeconds(0.6f);
        y = true;
        yield return new WaitForSeconds(0.6f);
    }
}
