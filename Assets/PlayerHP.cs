using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Text m_PlayerHPText;
    public Image m_PlayerSlider;

    public void Initialize()
    {
        m_PlayerHPText.text = "";
    }
    public void InitializeUp()
    {
        m_PlayerHPText.text = string.Format("{0} / 100", InfoMng.GetIns.PlayerHP, 100);
        m_PlayerSlider.fillAmount = InfoMng.GetIns.PlayerHP / 100;
    }


}
