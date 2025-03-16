using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveProjectileBehaviour : ProjectileWeaponBehaviour
{
    protected void Update()
    {
        transform.position += transform.forward * Time.deltaTime * weaponData.Speed;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}

