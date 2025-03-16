using UnityEngine;

public class Ogr : AverageEnemy
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Attack()
    {
        if (base._playerIDamagable != null && !base.isAttacking)
        {
            base.Attack();
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

    }
    protected override void FallDrop()
    {
        EnemyDrop drop = GetComponent<EnemyDrop>();
        drop.FallDrop(transform.position, Drop.red);
    }

}
