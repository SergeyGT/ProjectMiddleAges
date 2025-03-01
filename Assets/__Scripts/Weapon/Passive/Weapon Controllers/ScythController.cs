using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScythController : PassiveWeaponController
{
    protected override void Attack()
    {
        base.Attack();

        PoolManager.SpawnObject(_weapon, transform)
            .GetComponent<ScythBehaviour>()
            .StartAnim();

    }
}
