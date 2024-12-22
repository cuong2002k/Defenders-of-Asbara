using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private PlayAudioControl _playAudioControl;
    private void OnEnable()
    {
        if (_playAudioControl == null)
            _playAudioControl = GetComponent<PlayAudioControl>();

        _playAudioControl.PlayAudio(SoundType.HIT);
    }


}
