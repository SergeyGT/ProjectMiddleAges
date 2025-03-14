using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundManager : MonoBehaviour
{
    [Header("Set Sound Of Object")]
    [SerializeField] private AudioClip _audioClip;
    
    private AudioSource _audioSource;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void PlaySound()
    {
        if( _audioSource != null && _audioClip != null)
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}
