using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{
    [SerializeField] Animator m_stage;
    [SerializeField] Animator m_panel;
    [SerializeField] Animator m_Option;
    [SerializeField] Animator m_Multy;
    public void StartGame()
    {
        StartCoroutine("GameStart");
    }
    public void Multy1vs1()
    {
        StartCoroutine("Start1vs1");
    }
    public void ExitGameOKSelete()
    {
        Application.Quit();
    }
    public void OptionIn()
    {

        StartCoroutine("GameOptionIn");

    }
    public void optionOut()
    {

        StartCoroutine("GameOptionOut");
    }
    public void BackGame()
    {
        StartCoroutine("Back");
    }

    public void MultyIn()
    {
        StartCoroutine("MultyI");

    }

    public void Multyout()
    {
        StartCoroutine("MultyO");

    }
    public void ExitGameNOSelete()
    {
    }
    IEnumerator Start1vs1()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("LobbyScene");
    }
    IEnumerator MultyI()
    {
        StopCoroutine("Back");
        m_panel.SetTrigger("IsAni");
        yield return new WaitForSeconds(0.5f);
        m_Multy.SetTrigger("IsIn");
    }    
    IEnumerator MultyO()
    {
        StopCoroutine("GameStart");
        m_Multy.SetTrigger("IsOut");
        yield return new WaitForSeconds(0.5f);
        m_panel.SetTrigger("IsAniOut");
    }

    IEnumerator GameOptionIn()
    {
        StopCoroutine("Back");
        m_panel.SetTrigger("IsAni");
        yield return new WaitForSeconds(0.5f);
        m_stage.SetTrigger("IsIn");
    }
    IEnumerator GameOptionOut()
    {
        StopCoroutine("Back");
        m_panel.SetTrigger("IsAni");
        yield return new WaitForSeconds(0.5f);
        m_stage.SetTrigger("IsIn");
    }
    IEnumerator GameStart()
    {
        StopCoroutine("Back");
        m_panel.SetTrigger("IsAni");
        yield return new WaitForSeconds(0.5f);
        m_stage.SetTrigger("IsIn");
        yield return new WaitForSeconds(0.5f);
        GameMng.GetIns.TitleMenwStageSelete = true;
    }
    IEnumerator Back()
    {
        StopCoroutine("GameStart");
        m_stage.SetTrigger("IsOut");
        yield return new WaitForSeconds(0.5f);
        m_panel.SetTrigger("IsAniOut");
    }
}
