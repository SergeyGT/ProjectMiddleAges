using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected GameObject _weapon;

    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    protected virtual void Start()
    {
        PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected abstract void Attack();
}
