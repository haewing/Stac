using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] Animator m_stage;
    [SerializeField] Animator m_panel;
    [SerializeField] Animator m_Option;
    public void StartGame()
    {
        StartCoroutine("GameStart");
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
    public void ExitGameNOSelete()
    {
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
    }
    IEnumerator Back()
    {
        StopCoroutine("GameStart");
        m_stage.SetTrigger("IsOut");
        yield return new WaitForSeconds(0.5f);
        m_panel.SetTrigger("IsAniOut");
    }
}
