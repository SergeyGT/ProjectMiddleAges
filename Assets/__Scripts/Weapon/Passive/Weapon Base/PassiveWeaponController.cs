using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveWeaponController : WeaponController
{
    private float _currentCooldown;

    protected override void Start()
    {
        base.Start();
        _currentCooldown = weaponData.CooldownDuration; //� ������ ���� �� ���������
    }


    private void Update()
    {
        //�������� �� unitask
        _currentCooldown -= Time.deltaTime;
        if (_currentCooldown <= 0)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        _currentCooldown = weaponData.CooldownDuration;
    }
}
