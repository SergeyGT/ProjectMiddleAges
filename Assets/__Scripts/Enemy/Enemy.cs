using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected float _hp = 100;
    protected float _damage  = 10;
    protected float _speedAttack = 3;
    protected GameObject _weapon;
    protected Transform _playerPosition;
    protected NavMeshAgent _agent;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerPosition = GameObject.FindWithTag("Player").transform;
    }

    protected void Init(int hp, int damage, float speedAttack)
    {
        _hp = hp;
        _damage = damage;
        _speedAttack = speedAttack;
    }

    protected virtual void Move()
    {
        if (_playerPosition != null)
        {
            _agent.SetDestination(_playerPosition.position);
        } else
        {
            _agent.isStopped = true;
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Kill();
            FallDrop(transform.position, Drop.blue);
        }
    }

    protected abstract void Attack();

    protected void Kill()
    {
        Destroy(this.gameObject);
    }

    protected virtual void FallDrop(Vector3 pos, Drop _drop)
    {
        EnemyDrop drop = GetComponent<EnemyDrop>();
        drop.FallDrop(pos, _drop);
    }
}
