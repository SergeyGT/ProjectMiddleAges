using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : PassiveWeaponController
{
    protected override void Attack()
    {
        base.Attack();

        PoolManager.SpawnObject(weaponData.weapon, transform)
            .GetComponent<SwordBehaviour>()
            .MakeAttack();

    }
}
