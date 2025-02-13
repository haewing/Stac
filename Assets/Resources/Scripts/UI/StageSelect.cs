using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    
    public static StageSelect Instance;
    [SerializeField] Image[] LockImage;
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
    private void Update()
    {

    }
    public void Stage1()
    {

        if (InfoMng.GetIns.StageLock[0] && GameMng.GetIns.TitleMenwStageSelete)
            SceneManager.LoadScene("Stage1");
    }
    public void Stage2()
    {
        if (InfoMng.GetIns.StageLock[1] && GameMng.GetIns.TitleMenwStageSelete)
         SceneManager.LoadScene("Stage2");
    }
    public void Stage3()
    {
        if (InfoMng.GetIns.StageLock[2] && GameMng.GetIns.TitleMenwStageSelete)
         SceneManager.LoadScene("Stage3");
    }

    public void StageH(string Stage, float a, int i)
    {
        StartCoroutine(StageClick(Stage, a, i));
    }
    IEnumerator StageClick(string Stage ,float alpha,int idx)
    {

   
        {
            if (!InfoMng.GetIns.StageLock[idx - 1])
            {

                   while (alpha > 0)
                {
                    alpha -= Time.deltaTime * 2;
                    LockImage[idx-2].color = new Color(1, 1, 1, alpha);

                    yield return new WaitForSeconds(0.01f);
                }
            }

        }
        InfoMng.GetIns.StageLock[idx - 1] = true;
        
    }
}
