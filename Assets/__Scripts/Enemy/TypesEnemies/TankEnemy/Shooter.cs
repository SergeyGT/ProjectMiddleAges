using UnityEngine;

public class Shooter : TankEnemy
{
    [Header("Set In Inspector Stats Shooter")]
    
    [SerializeField] private int _shooterRadiusAttack;

    private float _distanceBetweenPlayer;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Move()
    {
        if (_playerPosition == null) return;

        _distanceBetweenPlayer = (transform.position - _playerPosition.position).magnitude;
        if (_distanceBetweenPlayer > _shooterRadiusAttack) { base.Move(); base._agent.isStopped = false; }
        else base._agent.isStopped = true;        
    }

    protected override void Attack()
    {
        if (base._playerIDamagable != null && !base.isAttacking && _agent.isStopped)
        {
            base.Attack();
            base._playerIDamagable.TakeDamage(_damage);
            StartCoroutine(base.DelayAttack(_speedAttack));
        }
    }
    

    private void FixedUpdate()
    {
        Move();
        if (base._hp > 0)
        {
            Attack();
        }
    }

    protected override void FallDrop()
    {
        EnemyDrop drop = GetComponent<EnemyDrop>();
        drop.FallDrop(transform.position, Drop.green);
    }
}
