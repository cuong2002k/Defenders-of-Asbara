using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : TowerBase
{
    private PlayAudioControl _playAudioControl;
    protected override void Awake()
    {
        base.Awake();
        _playAudioControl = GetComponent<PlayAudioControl>();
    }
    protected override void Shoot()
    {
        if (this._bulletPrefabs == null)
        {
            Common.LogWarning("Bullet prefab not found in {0}", this.gameObject);
            return;
        }
        Transform firstTarget = this._targetter.GetFirstTarget();
        Transform attackTranform = this.GetFirstAttackPoint();
        GameObject bulletInstance = this.SpawnPrefabs(this._bulletPrefabs, attackTranform.position);
        bulletInstance.GetComponent<BulletBase>().Initialized(firstTarget, finalDamage);
        bulletInstance.GetComponent<IPoolAble>().OnSpawn();
        _playAudioControl.PlayAudio(SoundType.ATTACK);
        // Spawn Muzzle
        GameObject muzzleInstance = this.SpawnPrefabs(this._muzzle, attackTranform.position);
        muzzleInstance.transform.rotation = Quaternion.LookRotation(attackTranform.forward);
    }
}
