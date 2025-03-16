using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    [Header("Stats Enemy")]
    [SerializeField] protected int _hp = 100;
    [SerializeField] protected int _damage  = 10;
    [SerializeField] protected float _speedAttack = 3;
    [Space]
    [Header("Source Enemy")]
    [SerializeField] AudioSource _enemySource;
    [Header("Audio Clips Enemy")]
    [SerializeField] AudioClip _attack;
    [SerializeField] AudioClip _walk;
    [SerializeField] AudioClip _death;
    protected GameObject _weapon;
    protected Transform _playerPosition;
    protected NavMeshAgent _agent;
    protected bool _collidedPlayer = false;
    protected IDamagable _playerIDamagable;
    protected bool isAttacking = false;
    protected Animator _animator;
   
    private CapsuleCollider _capsuleCollider;
    private bool _isDead = false;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Init(Transform playerTransform, IDamagable playerDamagable)
    {
        _playerPosition = playerTransform;
        _playerIDamagable = playerDamagable;
    }

    protected virtual void Move()
    {
        if (_isDead) return;        

        if (_playerPosition != null)
        {
            if (!_enemySource.isPlaying)
            {
                _enemySource.pitch = 3f;
                SoundManager.Instance.PlayLocalSound(_enemySource, _walk);
            }
            _animator.SetBool("Walk", true);
            _agent.SetDestination(_playerPosition.position);
        } else
        {
            _animator.SetBool("Walk", false);
            _agent.isStopped = true;
        }
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            SoundManager.Instance.PlayLocalSound(_enemySource, _death);
            Kill();
            FallDrop();
        }
    }

    protected virtual void Attack()
    {
        if (!_enemySource.isPlaying)
        {
            print("Attack sound");
            _enemySource.pitch = 1f;
            _enemySource.volume = 1f;
            SoundManager.Instance.PlayLocalSound(_enemySource, _attack);
        }
    }

    protected void Kill()
    {
        _isDead = true;
        _agent.isStopped = true;
        _agent.enabled = false;
        _capsuleCollider.enabled = false;
        StartCoroutine(DelayDeath());
    }

    protected abstract void FallDrop();

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collidedPlayer = true;
            _playerIDamagable = collision.gameObject.GetComponent<IDamagable>();
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collidedPlayer = false;
            _playerIDamagable = null;
            if (_agent.enabled && _agent.isOnNavMesh)
            {
                _agent.isStopped = false;
                _animator.SetBool("Attack", false);  
                _animator.SetBool("Walk", true);    
            }
        }
    }


    protected IEnumerator DelayAttack(float _delayAttack)
    {
        isAttacking = true;
        yield return new WaitForSeconds(_delayAttack);
        isAttacking = false;
    }

    protected IEnumerator DelayDeath()
    {
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
