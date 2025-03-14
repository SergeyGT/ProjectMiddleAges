using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundManager : MonoBehaviour
{
    [Header("Set Sound Of Object")]
    [SerializeField] private AudioClip _audioClip;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual PlaySound()
    {
        if( _audioSource != null && _audioClip != null)
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}
