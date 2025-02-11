using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // «атычка до по€вл€ени€ других классов врагов
    

    protected int _hp;
    protected int _damage;
    protected int _speedAtack;
    protected GameObject _weapon;

    private Transform _playerPosition;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerPosition = GameObject.Find("Player").transform;
    }

    private void Move()
    {
        _agent.SetDestination(_playerPosition.position);
    }

    protected void Attack()
    {

    }

    private void Kill()
    {
        //Make Drop
        Destroy(this.gameObject);
    }

    private void Update()
    {
        Move();
        Attack();

        if(_hp <= 0)
        {
            Kill();
        }
    }
}
