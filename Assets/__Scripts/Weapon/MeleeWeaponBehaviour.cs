using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeaponBehaviour : WeaponBehaviour
{
    public WeaponScriptableObject weaponData;
    public abstract void MakeAttack();

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
