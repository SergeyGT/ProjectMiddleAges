using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveWeaponController : WeaponController
{
    public Vector3 ShootDirection { get; private set; }

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
        ShootDirection = transform.forward;
        PoolManager.SpawnObject(_weapon, transform, PoolManager.PoolType.Projectiles);
    }
}
