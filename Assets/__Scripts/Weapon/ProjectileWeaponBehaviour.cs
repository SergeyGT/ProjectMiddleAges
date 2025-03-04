using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : WeaponBehaviour
{
    public Vector3 Direction { get; protected set; }
    [field: SerializeField] public float Speed { get; private set; }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Environment"))
        {
            PoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
