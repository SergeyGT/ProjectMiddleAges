using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveWeaponController : WeaponController
{
    [field: SerializeField] public float CooldownDuration { get; private set; }

    private float _currentCooldown;

    protected override void Start()
    {
        base.Start();
        _currentCooldown = CooldownDuration; //В начале игры кд дефолтное
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

    protected override void Attack()
    {
        _currentCooldown = CooldownDuration;
    }
}
