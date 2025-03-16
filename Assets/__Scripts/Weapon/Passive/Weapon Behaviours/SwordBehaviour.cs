using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MeleeWeaponBehaviour
{   
    public override void MakeAttack()
    {
        transform.forward = transform.right;

        DOTween.Sequence()
            .Append(transform.DORotate(transform.up * -180, weaponData.Duration, RotateMode.LocalAxisAdd))
            .SetLoops(weaponData.Repetings * 2, LoopType.Yoyo)
            .SetEase(Ease.InCubic)
            .OnComplete(()=> PoolManager.ReturnObjectToPool(gameObject));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
