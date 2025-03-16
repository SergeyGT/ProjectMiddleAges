using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : PassiveWeaponController
{
    protected override void Attack()
    {
        base.Attack();
        PoolManager.SpawnObject(weaponData.weapon, transform, PoolManager.PoolType.Projectiles)
            .GetComponent<AxeBehaviour>()
            .MakeAttack();
    }
}
