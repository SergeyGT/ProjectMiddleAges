using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : FastEnemy
{
    [Header("Set Stats Skeleton In Inspector")]
    [SerializeField] private int _skeletonHp = 100;
    [Space]
    [SerializeField] private int _skeletonDamage = 5;
    [Space]
    [SerializeField] private int _skeletonSpeedAttack = 5;   


    protected override void Awake()
    {
        Init(_skeletonHp, _skeletonDamage, _skeletonSpeedAttack);
        base.Awake();
    }

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(90,0,0);
    }
    protected override void Attack()
    {
        if (base._playerIDamagable != null && !base.isAttacking)
        {
            base._playerIDamagable.TakeDamage(_skeletonDamage);
            StartCoroutine(base.DelayAttack(_skeletonSpeedAttack));
        }
    }

    private void FixedUpdate()
    {
        base.Move();
        if (_collidedPlayer)
        {
            Attack();
        }
        if(_skeletonHp == 0)
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
