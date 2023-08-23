using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossClear : MonoBehaviour
{


    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.name == "GameClear")
        {
            InfoMng.GetIns.StageName = SceneManager.GetActiveScene().name;
            StartCoroutine("Fade");
        }
    }
    public Image m_Fade;
    float alpha = 0;
    IEnumerator Fade()
    {
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            m_Fade.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);

        }
        SceneManager.LoadScene("Title");
    }
}
