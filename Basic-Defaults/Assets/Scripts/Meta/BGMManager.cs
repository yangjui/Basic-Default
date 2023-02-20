using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : Singleton<BGMManager>
{

    // ����Ҹ� �߼Ҹ��� ������Ʈ�� ������

    [SerializeField]
    AudioClip defaultAudio;
    [SerializeField]
    AudioClip doorOpenAudio;
    [SerializeField]
    AudioClip whisperAudio;
    [SerializeField]
    AudioClip birdTwitterAudio;

    AudioSource m_audio;

    public void Awake()
    {
        m_audio = GetComponent<AudioSource>();
    }

    public void PlayDoorOpenAudio()
    {
        m_audio.clip = doorOpenAudio;
        m_audio.Play();
    }
    public void PlayWhispergAudio()
    {
        m_audio.clip = whisperAudio;
        m_audio.Play();
    }
    public void BirdTwitterAudio()
    {
        m_audio.clip = birdTwitterAudio;
        m_audio.Play();
    }

    public void PlayDefaultAudio() // ������ �Ҹ�
    {
        m_audio.clip = defaultAudio;
        m_audio.Play();
    }
}