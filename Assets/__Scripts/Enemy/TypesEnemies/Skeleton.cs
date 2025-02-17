using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [Header("Set Stats Skeleton In Inspector")]
    [SerializeField] private int _skeletonHp = 100;
    [Space]
    [SerializeField] private int _skeletonDamage = 5;
    [Space]
    [SerializeField] private int _skeletonSpeedAttack = 1;
    private bool _collidedPlayer = false;


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
        //Нанесение уронов ГГ
        StartCoroutine(DelayAttack(_skeletonSpeedAttack));
    }

    private IEnumerator DelayAttack (float _delayAttack)
    {
        yield return new WaitForSeconds(_delayAttack);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collidedPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collidedPlayer = false;
        }
    }

    private void FixedUpdate()
    {
        base.Move();
        if (_collidedPlayer)
        {
            Attack();
        }
    }
    protected override void FallDrop(Drop _drop)
    {
        base.FallDrop(Drop.blue);
    }
}
