using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Text m_Text;
    public string[] m_Dialogue;
    string[] dialogues;
    public int talkNum;
    // Start is called before the first frame update
    void Start()
    {
        StartTalk(m_Dialogue);
    }
    public void StartTalk(string[] talk)
    {
        m_Text.text = "";
        dialogues = talk;

        StartCoroutine(Typing(dialogues[talkNum]));
    }
    public void NextTalk()
    {

        talkNum++;

        if(talkNum == dialogues.Length)
        {
            endTalk();
            return;
        }
        StartCoroutine(Typing(dialogues[talkNum]));
    }
    public void endTalk()
    {
        talkNum = 0;

    }
    IEnumerator Typing(string talk)
    {
        //m_Text.text = "";
        if (talkNum == 4) 
        { 
            m_Text.text = "";
            m_Text.fontSize = 150;
            m_Text.alignment = TextAnchor.MiddleCenter;
        }
        if (talk.Contains("  ")) talk = talk.Replace("  ", "\n");
        for (int i = 0; i < talk.Length; i++)
        {
            
            m_Text.text += talk[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.75f);
        NextTalk();
    }
}
