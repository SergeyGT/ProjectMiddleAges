using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveWeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected GameObject _weapon;

    [field:SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float CooldownDuration { get; private set; }

    [SerializeField] private float _currentCooldown;
    public PlayerMovement PlayerMovement { get; private set; }



    private int pirce;
    private void Start()
    {
        _currentCooldown = CooldownDuration; //В начале игры кд дефолтное
        PlayerMovement = FindObjectOfType<PlayerMovement>();
    }


    private void Update()
    {
        //заменить на unitask
        _currentCooldown -= Time.deltaTime;
        if (_currentCooldown <= 0)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        _currentCooldown = CooldownDuration;
    }

}
