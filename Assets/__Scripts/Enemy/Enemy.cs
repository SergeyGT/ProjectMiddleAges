using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected virtual int _hp { get; set; } =  100;
    protected virtual int _damage { get; set; } = 10;
    protected virtual int _speedAtack { get; set; } = 3;
    protected  GameObject _weapon;
    protected Transform _playerPosition;
    protected NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerPosition = GameObject.Find("Player").transform;
    }

    private void Move()
    {
        _agent.SetDestination(_playerPosition.position);
    }

    public void TakeDamage(int damage)
    {
        _hp = Mathf.Max(0, _hp-damage);

        if (_hp == 0)
        {
            Kill();
            FallDrop();
        }
    }

    protected abstract void Attack();

    protected void Kill()
    {
        Destroy(this.gameObject);
    }

    protected virtual void FallDrop()
    {
        EnemyDrop drop = GetComponent<EnemyDrop>();
        drop.FallDrop(transform.position, Drop.blue);
    }

    private void Update()
    {
        Move();
        Attack();
    }


}
