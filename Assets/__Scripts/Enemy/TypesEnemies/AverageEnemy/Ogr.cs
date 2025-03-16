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
            _animator.SetBool("Walk", false);
            _animator.SetBool("Attack", true);
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
            _animator.SetBool("Walk", false);
        }

    }
    protected override void FallDrop()
    {
        EnemyDrop drop = GetComponent<EnemyDrop>();
        drop.FallDrop(transform.position, Drop.red);
    }

}
