using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMng : MonoBehaviour
{
    private static SoundMng instance = null;

    [SerializeField] Slider[] m_slider;
    public AudioSource[] m_Player;
    public AudioSource[] m_Enemy;
    public AudioSource[] m_Boss;

    public AudioSource[] m_SFX;
    public AudioSource m_bgm;

    void Awake()
    {

        m_slider[0].onValueChanged.AddListener(delegate { Bgm(); });
        m_slider[1].onValueChanged.AddListener(delegate { Sfx(); });
        
    }
    public static SoundMng Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    public void Bgm()
    {

        m_bgm.volume = m_slider[0].value;
    }   
    public void Sfx()
    {
        for (int i = 0; i < m_SFX.Length; i++)
        {

            m_SFX[i].volume += m_slider[1].value;
        }
        for (int i = 0; i < m_Boss.Length; i++)
        {
            m_Boss[i].volume += m_slider[1].value;
        }
        for (int i = 0; i < m_Enemy.Length; i++)
        {
            m_Enemy[i].volume += m_slider[1].value;
        }
        for (int i = 0; i < m_Player.Length; i++)
        {
            m_Player[i].volume += m_slider[1].value;
        }
    }
    public AudioSource Sound_SFX(int idx, bool Awake,bool Loop)
    {
        m_SFX[idx].playOnAwake = Awake;
        m_SFX[idx].loop = Loop;

        return m_SFX[idx];
    }
    public AudioSource Sound_Boss(int idx, bool Awake, bool Loop)
    {
        m_Boss[idx].playOnAwake = Awake;
        m_Boss[idx].loop = Loop;

        return m_Boss[idx];
    }
    public AudioSource Sound_Enemy(int idx, bool Awake, bool Loop)
    {
        m_Enemy[idx].playOnAwake = Awake;
        m_Enemy[idx].loop = Loop;

        return m_Enemy[idx];
    }
    public AudioSource Sound_Player(int idx, bool Awake, bool Loop)
    {
        m_Player[idx].playOnAwake = Awake;
        m_Player[idx].loop = Loop;

        return m_Player[idx];
    }


}
