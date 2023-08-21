using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillIcon : MonoBehaviour
{
    public Image Mod1;
    public Image Mod2;

    public Image Mask1;
    public Image Mask2;


    public TextMeshProUGUI Text1;
    public TextMeshProUGUI Text2;

    public Sprite Kunai;
    public Sprite KunaiSkill;
    public Sprite Knife;
    public Sprite KnifeSkill;

    float TimeCheck;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TimeCheck += Time.deltaTime;
        switch (GameMng.GetIns.PlayerAttackMode)
        {
            case 0:
                Mod1.sprite = Knife;
                Mod2.sprite = KnifeSkill;
                Mask1.fillAmount = GameMng.GetIns.KnifeCoolTime / 0.4f;
                if (GameMng.GetIns.KnifeCoolTime > 0) Text1.text = GameMng.GetIns.KnifeCoolTime.ToString("F1");
                else Text1.text = "";
                break;
            case 1:
                Mod1.sprite = Kunai;
                Mod2.sprite = KunaiSkill;
                Mask1.fillAmount = GameMng.GetIns.KunaiCoolTime / 0.2f;
                if (GameMng.GetIns.KunaiCoolTime > 0) Text1.text = GameMng.GetIns.KunaiCoolTime.ToString("F1");
                else Text1.text = "";
                break;
        }



        Mask2.fillAmount = GameMng.GetIns.SkillCoolTime / 5;
        if (GameMng.GetIns.SkillCoolTime > 0) Text2.text = GameMng.GetIns.SkillCoolTime.ToString("F1");
        else Text2.text = "";


    }
}
