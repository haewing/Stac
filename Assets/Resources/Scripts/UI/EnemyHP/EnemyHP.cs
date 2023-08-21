using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] public GameObject SetHP;
    [SerializeField] private Image m_HP;
    [SerializeField] private Text m_HPText;
    [SerializeField] private Text _Name;


    bool a = false;
  
   
    public void Initialize()
    {

        SetHP.SetActive(false);

    }
    public void InitializeUp()
    {

    }
    public void EnemyMark(float MaxHP, float NowHP, string Name)
    {
        SetHP.SetActive(true);
        m_HP.fillAmount = NowHP / MaxHP;
        //m_HPText.text = string.Format("{0} / {1}", NowHP, MaxHP);
        _Name.text = Name;
    }
}
