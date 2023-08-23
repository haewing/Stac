using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] public GameObject SetHP;
    [SerializeField] private Image m_HPBack;
    [SerializeField] private Image m_HP;
    //[SerializeField] private Text m_HPText;
    //[SerializeField] private Text _Name;


  
   
    public void Initialize()
    {

    }
    public void InitializeUp()
    {
        Vector2 ThisPos = Camera.main.WorldToScreenPoint(GameObject.Find("Boss").transform.position);
        m_HP.transform.position = ThisPos;
        m_HPBack.transform.position = ThisPos;

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
    }
    
}
