using UnityEngine;

public class Shooter : TankEnemy
{
    [Header("Set In Inspector Stats Shooter")]
    [SerializeField] private int _shooterHp;
    [SerializeField] private int _shooterDamage;
    [SerializeField] private float _shooterDelayAttack;
    [SerializeField] private int _shooterRadiusAttack;

    private float _distanceBetweenPlayer;
    protected override void Awake()
    {
        Init(_shooterHp, _shooterDamage, _shooterDelayAttack);
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
            base._playerIDamagable.TakeDamage(_shooterDamage);
            StartCoroutine(base.DelayAttack(_shooterDelayAttack));
        }
    }
    

    private void FixedUpdate()
    {
        Move();
        if(base._hp > 0)
        {
            Attack();
        }

    }
}
