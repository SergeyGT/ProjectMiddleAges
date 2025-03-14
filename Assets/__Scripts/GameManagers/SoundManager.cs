using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Set Sound Of Object")]
    [SerializeField] private AudioSource _backSource;
    [SerializeField] private AudioSource _sfxSource;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// ��������������� ���������� ������(������)
    /// </summary>
    /// <param name="clip">����</param>
    /// <param name="loop">������ ���� �����������?</param>
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (_backSource.clip != clip)
        {
            _backSource.clip = clip;
            _backSource.loop = loop;
            _backSource.Play();
        }
    }

    /// <summary>
    /// ��������������� ��������
    /// </summary>
    /// <param name="_audioClip">����</param>
    public void PlaySound(AudioClip _audioClip)
    {
        if(_sfxSource != null)
        {
            _sfxSource.PlayOneShot(_audioClip);
        }
    }

    /// <summary>
    /// ��������������� ��������� ������ �������
    /// </summary>
    /// <param name="source">�������� ����� �������</param>
    /// <param name="clip">���� �������</param>
    public void PlayLocalSound(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null)
        {
            source.PlayOneShot(clip);
        }
    }
}
