using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyAuraController : PassiveWeaponController
{
    protected override void Attack()
    {
        base.Attack();
        PoolManager.SpawnObject(_weapon, transform);
        //GameObject go = Instantiate(_weapon);
        //go.transform.SetParent(transform, false);
    }
}
