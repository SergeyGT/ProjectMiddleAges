using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Vector3 Direction { get; protected set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    [SerializeField] private float _destroyAfterSeconds;
    protected virtual void Start()
    {
        Destroy(gameObject, _destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Environment"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy?.TakeDamage(Damage);
        }
    }
}
