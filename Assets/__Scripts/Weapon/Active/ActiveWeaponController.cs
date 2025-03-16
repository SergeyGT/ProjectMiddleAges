using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveWeaponController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }


    protected override void Attack()
    {
        PoolManager.SpawnObject(weaponData.weapon, transform, PoolManager.PoolType.Projectiles);
    }
}
