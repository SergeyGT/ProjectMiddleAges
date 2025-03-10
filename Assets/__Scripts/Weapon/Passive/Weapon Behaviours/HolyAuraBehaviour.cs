using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyAuraBehaviour : MeleeWeaponBehaviour
{
    public override void MakeAttack()
    {
        transform.DORotate(new Vector3(0,360, 0), ANIM_DURATION, RotateMode.LocalAxisAdd)
            .OnComplete(() => PoolManager.ReturnObjectToPool(gameObject));
    }
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("Holy ");
        base.OnTriggerEnter(other);
    }
}
