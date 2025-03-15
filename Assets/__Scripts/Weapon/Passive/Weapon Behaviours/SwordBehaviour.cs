using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MeleeWeaponBehaviour
{   
    [SerializeField]  private int _repetings = 1;

    public override void MakeAttack()
    {
        transform.forward = transform.right;

        DOTween.Sequence()
            .Append(transform.DORotate(transform.up * -180, ANIM_DURATION, RotateMode.LocalAxisAdd))
            .SetLoops(_repetings * 2, LoopType.Yoyo)
            .SetEase(Ease.InCubic)
            .OnComplete(()=> PoolManager.ReturnObjectToPool(gameObject));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
