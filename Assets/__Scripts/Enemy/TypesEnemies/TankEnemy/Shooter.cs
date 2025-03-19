using UnityEngine;

public class Shooter : TankEnemy
{
    [Header("Set In Inspector Stats Shooter")]
    
    [SerializeField] private int _shooterRadiusAttack;

    private bool _isAttackRadius = false;

    private float _distanceBetweenPlayer;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Move()
    {
        if (_playerPosition == null || base._agent == null) return;

        _distanceBetweenPlayer = (transform.position - _playerPosition.position).magnitude;

        if (!base._agent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent не на NavMesh!");
            return;
        }

        if (_distanceBetweenPlayer > _shooterRadiusAttack)
        {
            _isAttackRadius = false;
            base._agent.speed = base._speedMove;
            base.Move();
        }
        else _isAttackRadius = true;
    }


    protected override void Attack()
    {
        if (base._playerIDamagable != null && !base.isAttacking)
        {
            base._agent.speed = 0;
            base.Attack();
            _animator.SetBool("Walk", false);
            _animator.SetBool("Attack", true);
            print("Shooter Attack");
            base._playerIDamagable.TakeDamage(_damage);
            StartCoroutine(base.DelayAttack(_speedAttack));     
        }
    }
    

    private void FixedUpdate()
    {
        Move();
        if (_isAttackRadius) 
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
