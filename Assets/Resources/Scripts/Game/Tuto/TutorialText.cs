using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Text;
    [SerializeField] PlayerController m_Player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextState());
    }
    int TextMod = 0;
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TextState()
    {
        while (TextMod <= 7)
        {
            switch (TextMod)
            {
                case 0:
                    m_Text.text = string.Format("<- 'A'  LEFT\n         RIGHT  'D'->");
                    if (m_Player.Tu_IsAxis)
                    {
                        TextMod = 1;
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 1:
                    m_Text.text = string.Format("'Space' : Jump");
                    if (m_Player.Tu_IsJump)
                    {
                        TextMod = 2;
                        yield return new WaitForSeconds(1f);
                    }     
                    break;
                case 2:

                    m_Text.text = string.Format("LEFT Click : Att");
                    if (m_Player.Tu_Att)
                    {
                        TextMod = 3;
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 3:

                    m_Text.text = string.Format("'1' : Melee");
                    if (m_Player.Tu_Melee)
                    {
                        TextMod = 4;
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 4:
                    m_Text.text = string.Format("'2' : Far");
                    if (m_Player.Tu_Far)
                    {
                        TextMod = 5;
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 5:
                    m_Text.text = string.Format("'E' : Skill");
                    if (m_Player.Tu_IsSkill)
                    {
                        TextMod = 6;
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 6:
                    m_Text.text = string.Format("Let's go!");
                    TextMod = 7;
                    yield return new WaitForSeconds(1f);
                    break;
                default:
                    m_Text.text = ""; 
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void ModChange()
    {


    }
    
}
