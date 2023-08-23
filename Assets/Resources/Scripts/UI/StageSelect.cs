using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
 
    public static StageSelect Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Stage1()
    {
        if (!InfoMng.GetIns.StageLock[0]) StartCoroutine(StageClick("lock1", 1,0));
        else SceneManager.LoadScene("Stage1");
    }
    public void Stage2()
    {
        if (!InfoMng.GetIns.StageLock[1]) StartCoroutine(StageClick("lock2", 1,1));
        else SceneManager.LoadScene("Stage2");
    }
    public void Stage3()
    {
        if (!InfoMng.GetIns.StageLock[2]) StartCoroutine(StageClick("lock3", 1,2));
        else SceneManager.LoadScene("Stage3");
    }
    IEnumerator StageClick(string Stage ,float alpha,int idx)
    {
        Image stage = GameObject.Find(Stage).GetComponent<Image>();
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 2;
            stage.color = new Color(1, 1, 1, alpha);

            yield return new WaitForSeconds(0.01f);
        }
        InfoMng.GetIns.StageLock[idx] = true;
        
    }
}
