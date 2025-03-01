using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    [SerializeField] protected GameObject _weapon;
    public PlayerMovement PlayerMovement { get; private set; }
    protected virtual void Start()
    {
        PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected abstract void Attack();
}
