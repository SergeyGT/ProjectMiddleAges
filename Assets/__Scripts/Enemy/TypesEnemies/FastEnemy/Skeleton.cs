using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : FastEnemy
{


    protected override void Awake()
    {
        Init(_hp, _damage, _speedAttack);
        base.Awake();
    }

    protected override void Attack()
    {
        if (base._playerIDamagable != null && !base.isAttacking)
        {
            base._playerIDamagable.TakeDamage(_damage);
            StartCoroutine(base.DelayAttack(_speedAttack));
        }
    }

    private void FixedUpdate()
    {
        base.Move();
        if (_collidedPlayer)
        {
            Attack();
        }
        if(_hp <= 0)
        {
            base.Kill();
            FallDrop(transform.position, Drop.blue);
        }
    }
    protected override void FallDrop(Vector3 pos, Drop _drop)
    {
        base.FallDrop(pos, _drop);
    }

}
