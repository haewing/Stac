using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    public EnemyHP m_Enemy;
    public PlayerHP m_PlayerHP;
    public Image m_Fade;

    // Start is called before the first frame update
    public void Initialize()
    {
        m_Enemy.Initialize();
        //m_PlayerHP.Initialize();
        //StartCoroutine("Fade");
    }
    public void InitializeUp()
    {
        //m_Enemy.InitializeUp();
        //m_PlayerHP.InitializeUp();
    }
    float alpha = 1;
    IEnumerator Fade()
    {
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            m_Fade.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);

        }
    }
}
