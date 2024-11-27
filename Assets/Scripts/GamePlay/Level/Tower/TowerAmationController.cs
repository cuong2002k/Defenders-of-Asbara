using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAmationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private float _transitionDuration = 0.3f;

    private void Awake()
    {
        this._animator = GetComponent<Animator>();
    }

    private void PlayAnimation(string name)
    {
        if (this._animator == null) return;
        this._animator.CrossFade(name, _transitionDuration);
    }

    public void PlayAttackAnimation()
    {
        this.PlayAnimation("Attack");
    }

    public void ResetAnimation()
    {
        this.PlayAnimation("Normal");
    }

    public void SetAnimationSpeed(int speed)
    {
        this._animator.speed = speed;
    }
}
