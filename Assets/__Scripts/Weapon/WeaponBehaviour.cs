using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Environment"))
        {
            IDamagable enemy = other.gameObject.GetComponent<IDamagable>();
            enemy?.TakeDamage((int)weaponData.Damage);
        }
    }


}
