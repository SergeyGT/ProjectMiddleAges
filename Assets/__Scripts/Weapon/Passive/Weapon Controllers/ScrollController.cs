using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : PassiveWeaponController
{
    public Transform Target {  get; private set; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Target = null;
        }
    }


    protected override void Attack()
    {
        if (Target!=null)
        {
            base.Attack();
            PoolManager.SpawnObject(_weapon, transform, PoolManager.PoolType.Projectiles)
                .GetComponent<ScrollBehaviour>()
                .SetTargetTransform(Target);
        }
    }
}
