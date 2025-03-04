using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MeleeWeaponBehaviour
{   
    [SerializeField]  private int _repetings = 1;

    public void MakeAttack()
    {
        transform.forward = transform.right;

        float splashDuration = (float)(DestroyAfterSeconds / (_repetings * 2));
        DOTween.Sequence()
            .Append(transform.DORotate(transform.up * -180, splashDuration, RotateMode.LocalAxisAdd))
            .SetLoops(_repetings * 2, LoopType.Yoyo)
            .SetEase(Ease.InCubic);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
