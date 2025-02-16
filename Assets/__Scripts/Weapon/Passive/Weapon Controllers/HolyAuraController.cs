using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyAuraController : PassiveWeaponController
{
    protected override void Attack()
    {
        base.Attack();
        Instantiate(_weapon, transform);
    }
}
