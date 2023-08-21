using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMng : MonoBehaviour
{
    private static SoundMng instance = null;

    public AudioSource[] m_Player;
    public AudioSource[] m_Enemy;
    public AudioSource[] m_Boss;

    public AudioSource[] m_SFX;
    public AudioSource m_bgm;
    [Range(0, 5)] public float SoundSize;

    void Awake()
    {

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
    private void Update()
    {
        AudioSoundControl();
    }
    void AudioSoundControl()
    {
        AudioListener.volume = SoundSize;
        
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
