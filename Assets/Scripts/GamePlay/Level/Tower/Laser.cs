using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Laser : TowerBase
{

    private PlayAudioControl _playAudioControl;
    protected override void Awake()
    {
        base.Awake();
        _playAudioControl = GetComponent<PlayAudioControl>();
    }


    protected override void Shoot()
    {
        Transform attackTranform = this.GetFirstAttackPoint();
        GameObject bulletInstance = this.SpawnPrefabs(this._bulletPrefabs, attackTranform.position);
        bulletInstance.GetComponent<BulletBase>().Initialized(this.finalDamage);
        _playAudioControl.PlayAudio(SoundType.ATTACK);
        bulletInstance.transform.SetParent(this._targetRotation.Turret);
        bulletInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);
        bulletInstance.transform.position = attackTranform.position;
        bulletInstance.transform.localScale = Vector3.one;
    }
}
