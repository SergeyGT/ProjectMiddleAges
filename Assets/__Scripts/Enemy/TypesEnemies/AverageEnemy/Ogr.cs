using UnityEngine;

public class Ogr : AverageEnemy
{
    [Header("Set Stats Ogr In Inspector")]
    [SerializeField] private int _ogrHp = 100;
    [Space]
    [SerializeField] private int _ogrDamage = 5;
    [Space]
    [SerializeField] private int _ogrSpeedAttack = 5;

    protected override void Awake()
    {
        Init(_ogrHp, _ogrDamage, _ogrSpeedAttack);
        base.Awake();
    }

    protected override void Attack()
    {
        if (base._playerIDamagable != null && !base.isAttacking)
        {
            base._playerIDamagable.TakeDamage(_ogrDamage);
            StartCoroutine(base.DelayAttack(_ogrSpeedAttack));
        }
    }

    private void FixedUpdate()
    {
        base.Move();
        if (_collidedPlayer)
        {
            Attack();
        }
        if (_ogrHp <= 0)
        {
            base.Kill();
            FallDrop(transform.position, Drop.green);
        }
    }
    protected override void FallDrop(Vector3 pos, Drop _drop)
    {
        base.FallDrop(pos, _drop);
    }

}
