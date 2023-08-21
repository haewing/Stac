using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearDoor : MonoBehaviour
{
    public Image m_Fade;



    bool a = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            gameObject.transform.Translate(new Vector2(2 * Time.deltaTime, 0));

            Invoke("Fade", 2);
        }
    }
    void Fade()
    {
            m_Fade.color = new Color(0, 0, 0, m_Fade.color.a + Time.deltaTime);
            if(m_Fade.color.a > 1)
            {
                SceneManager.LoadScene("GameScene 1");
            }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (InfoMng.GetIns.GameClear) { 
            if (collision.gameObject.tag == "Player")
            {
                a = true;
            }
        }
    }
    



}
