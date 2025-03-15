using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeaponBehaviour : WeaponBehaviour
{
    [SerializeField] protected float ANIM_DURATION = 5;
    public abstract void MakeAttack();

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
