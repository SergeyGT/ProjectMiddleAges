using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheBehaviour : MeleeWeaponBehaviour
{

    private float INITIAL_ROTATE_DURATION = 0.1f;

    public override void MakeAttack()
    {
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, 90), INITIAL_ROTATE_DURATION))
            .Append(transform.DORotate(new Vector3(360, 0, 0), weaponData.Duration, RotateMode.LocalAxisAdd)
            .SetLoops(weaponData.Repetings)
            .SetEase(Ease.Linear)
            .OnComplete(() => PoolManager.ReturnObjectToPool(gameObject)));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
