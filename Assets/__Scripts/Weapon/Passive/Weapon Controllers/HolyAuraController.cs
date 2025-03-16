using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyAuraController : PassiveWeaponController
{
    protected override void Attack()
    {
        base.Attack();
        PoolManager.SpawnObject(weaponData.weapon, transform)
            .GetComponent<HolyAuraBehaviour>()
            .MakeAttack();
    }
}
