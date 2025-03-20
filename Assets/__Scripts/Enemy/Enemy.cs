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
    [SerializeField] protected int _speedMove = 3;
    [SerializeField] protected GameObject _weapon;
    [Space]
    [Header("Source Enemy")]
    [SerializeField] AudioSource _enemySource;
    [Header("Audio Clips Enemy")]
    [SerializeField] AudioClip _attack;
    [SerializeField] AudioClip _walk;
    [SerializeField] AudioClip _death;
    protected Transform _playerPosition;
    protected NavMeshAgent _agent;
    protected bool _collidedPlayer = false;
    protected IDamagable _playerIDamagable;
    protected bool isAttacking = false;
    protected Animator _animator;
   
    private CapsuleCollider _capsuleCollider;
    private bool _isDead = false;
    private Color[] _originalColors;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _agent.enabled = true;
    }

    public void Init(Transform playerTransform, IDamagable playerDamagable)
    {
        _playerPosition = playerTransform;
        _playerIDamagable = playerDamagable;
    }

    private void Start()
    {
        _agent.speed = _speedMove;
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
            _animator.SetBool("Attack", false);
            _animator.SetBool("Walk", true);
            if (_agent.isStopped)
            {
                _agent.isStopped = false;
            }
            _agent.SetDestination(_playerPosition.position);
        } 
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        StartCoroutine(Discarding());
        StartCoroutine(ChangeColorHit());

        if (_hp <= 0)
        {
            SoundManager.Instance.PlayLocalSound(_enemySource, _death);
            Kill();
            FallDrop();
        }
    }

    private IEnumerator Discarding()
    {
        _agent.speed = 0;

        yield return new WaitForSeconds(1.5f);

        _agent.speed = _speedMove;
    }

    private IEnumerator ChangeColorHit()
    {
        SkinnedMeshRenderer renderer = transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

        if (renderer != null)
        {
            if (_originalColors == null)
            {
                _originalColors = new Color[renderer.materials.Length];
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    _originalColors[i] = renderer.materials[i].color;
                }
            }
            
            foreach (Material mat in renderer.materials)
            {
                mat.color = Color.red;
            }
            
            yield return new WaitForSeconds(0.2f);
            
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                renderer.materials[i].color = _originalColors[i];
            }
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
