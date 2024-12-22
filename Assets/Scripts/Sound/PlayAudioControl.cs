using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioControl : MonoBehaviour
{
    private AudioSource _audioSource;
    public List<AudioConfig> AudioClips = new List<AudioConfig>();

    private void OnEnable()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayAudio(SoundType audioType)
    {
        AudioClip audioClip = null;
        foreach (AudioConfig audioConfig in AudioClips)
        {
            if (audioConfig.SoundType == audioType)
            {
                audioClip = audioConfig.AudioClip;
            }
        }

        if (audioClip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}

public enum SoundType
{
    ATTACK,
    HIT
}
