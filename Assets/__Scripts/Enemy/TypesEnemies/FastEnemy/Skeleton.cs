using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : FastEnemy
{
    [Header("Set Stats Skeleton In Inspector")]
    [SerializeField] private int _skeletonHp = 100;
    [Space]
    [SerializeField] private int _skeletonDamage = 5;
    [Space]
    [SerializeField] private int _skeletonSpeedAttack = 5;

    [SerializeField] private bool _collidedPlayer = false;
    private IDamagable _playerIDamagable;
    private bool isAttacking = false;


    protected override void Awake()
    {
        Init(_skeletonHp, _skeletonDamage, _skeletonSpeedAttack);
        base.Awake();
    }

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(90,0,0);
    }
    protected override void Attack()
    {
        if (_playerIDamagable != null && !isAttacking)
        {
            _playerIDamagable.TakeDamage(_skeletonDamage);
            StartCoroutine(DelayAttack(_skeletonSpeedAttack));
        }
    }

    private IEnumerator DelayAttack (float _delayAttack)
    {
        isAttacking = true;
        yield return new WaitForSeconds(_delayAttack);
        isAttacking = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collidedPlayer = true;
            _playerIDamagable = collision.gameObject.GetComponent<IDamagable>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collidedPlayer = false;
            _playerIDamagable = null;
        }
    }

    private void FixedUpdate()
    {
        base.Move();
        if (_collidedPlayer)
        {
            Attack();
        }
        if(_skeletonHp == 0)
        {
            base.Kill();
            FallDrop(transform.position, Drop.blue);
        }
    }
    protected override void FallDrop(Vector3 pos, Drop _drop)
    {
        base.FallDrop(pos, Drop.blue);
    }

}
