using UnityEngine;

public class Shooter : TankEnemy
{
    [Header("Set In Inspector Stats Shooter")]
    
    [SerializeField] private int _shooterRadiusAttack;

    private bool isWalk = false;

    private float _distanceBetweenPlayer;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Move()
    {
        if (_playerPosition == null || base._agent == null) return;

        _distanceBetweenPlayer = (transform.position - _playerPosition.position).magnitude;

        if (!base._agent.enabled || !base._agent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent отключен или не на NavMesh!");
            return;
        }

        if (_distanceBetweenPlayer > _shooterRadiusAttack)
        {
            isWalk = true;
            base.Move();
            base._agent.isStopped = false;
        }
        else
        {            
            isWalk = false;
            base._agent.isStopped = true;
            Attack();
        }
    }


    protected override void Attack()
    {
        if (base._playerIDamagable != null && !base.isAttacking && _agent.isStopped)
        {
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
    }

    protected override void FallDrop()
    {
        EnemyDrop drop = GetComponent<EnemyDrop>();
        drop.FallDrop(transform.position, Drop.green);
    }
}
