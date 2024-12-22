using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fire : TowerBase
{
    private PlayAudioControl _playAudioControl;
    protected override void Awake()
    {
        base.Awake();
        _playAudioControl = GetComponent<PlayAudioControl>();
    }

    protected override void Shoot()
    {
        GameObject bulletInstance = this.SpawnPrefabs(this._bulletPrefabs, this.GetFirstAttackPoint().position);
        bulletInstance.transform.SetParent(this._targetRotation.Turret);
        _playAudioControl.PlayAudio(SoundType.ATTACK);
        bulletInstance.GetComponent<BulletBase>().Initialized(this.finalDamage);
        bulletInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
