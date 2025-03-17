using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveWeaponController : WeaponController
{
    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Gameplay
            && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }


    protected override void Attack()
    {
        PoolManager.SpawnObject(weaponData.weapon, transform, PoolManager.PoolType.Projectiles);
    }
}
