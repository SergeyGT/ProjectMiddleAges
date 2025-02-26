using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : PassiveWeaponController
{
    protected override void Attack()
    {
        base.Attack();
        PoolManager.SpawnObject(_weapon, transform.position, _weapon.transform.rotation, PoolManager.PoolType.Projectiles)
            .GetComponent<AxeBehaviour>()
            .MakeAnimSequence();
    }
}
