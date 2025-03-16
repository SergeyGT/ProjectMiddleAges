using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyAuraBehaviour : MeleeWeaponBehaviour
{
    public override void MakeAttack()
    {
        transform.DORotate(new Vector3(0,360, 0), weaponData.Duration, RotateMode.LocalAxisAdd)
            .OnComplete(() => PoolManager.ReturnObjectToPool(gameObject));
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
