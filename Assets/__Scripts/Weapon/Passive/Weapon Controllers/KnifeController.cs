using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : PassiveWeaponController
{
    protected override void Attack()
    {
        base.Attack();
        PoolManager.SpawnObject(_weapon, transform, PoolManager.PoolType.Projectiles);
    }
}
