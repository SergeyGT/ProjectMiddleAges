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
        else
        {
            _animator.SetBool("Attack", false);
            _agent.isStopped = false;
        }
    }
    protected override void FallDrop(Vector3 pos, Drop _drop)
    {
        base.FallDrop(pos, _drop);
    }

}
