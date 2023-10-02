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
            Invoke("Fade", 1);
        }
    }
    void Fade()
    {
            m_Fade.color = new Color(0, 0, 0, m_Fade.color.a + Time.deltaTime);
            if(m_Fade.color.a > 1)
            {
                SceneManager.LoadScene("Title");
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
