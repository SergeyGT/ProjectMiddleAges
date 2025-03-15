using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [field: SerializeField] public int Damage { get; set; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Environment"))
        {
            IDamagable enemy = other.gameObject.GetComponent<IDamagable>();
            enemy?.TakeDamage(Damage);
        }
    }


}
