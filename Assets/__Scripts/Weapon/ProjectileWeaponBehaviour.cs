using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : WeaponBehaviour
{
    [field: SerializeField] public float Speed { get; private set; }

    [SerializeField] private float PROJECTILE_DURATION = 5;

    private Coroutine _destroyAfterTimerCoroutine;

    private void OnEnable()
    {
        _destroyAfterTimerCoroutine = StartCoroutine(ReturnToPoolAfterTimer());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Environment"))
        {
            PoolManager.ReturnObjectToPool(gameObject);
        }
    }


    //«¿Ã≈Õ»“‹ Õ¿ UNITASK!!!!!
    private IEnumerator ReturnToPoolAfterTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < PROJECTILE_DURATION)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
