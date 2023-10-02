using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameOption : MonoBehaviour
{
    [SerializeField] Animator Option;
    [SerializeField] Text PlayTimeTxt;
    [SerializeField] Transform m_Perent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool State = false;


    public void ReTry()
    {
        if(m_Perent != null)
        {
            Transform[] childList = m_Perent.GetComponentsInChildren<Transform>();

            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    if (childList[i] != transform)
                        Destroy(childList[i].gameObject);
                }
            }

        }
        SceneManager.LoadScene(gameObject.scene.name);
    }
    public void GoMain()
    {
        if (m_Perent != null)
        {
            Transform[] childList = m_Perent.GetComponentsInChildren<Transform>();

            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    if (childList[i] != transform)
                        Destroy(childList[i].gameObject);
                }
            }

        }
        SceneManager.LoadScene("Title");

    }
    public void OptionMenwIn()
    {
        Option.SetTrigger("IsIn");
        State = true;
    }
    public void OptionMenwOut()
    {
        Option.SetTrigger("IsOut");
        State = false;
    }
    // Update is called once per frame
    void Update()
    {
        //PlayTimeTxt.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(GameMng.GetIns.PlayTime / 60), Mathf.FloorToInt(GameMng.GetIns.PlayTime % 60));

        for (int i = 0; i < 1; i++)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !State)
            {
                Option.SetTrigger("IsIn");
                State = true;
                break;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && State)
            {
                Option.SetTrigger("IsOut");
                State = false;
                break;
            }

        }
    }
}
