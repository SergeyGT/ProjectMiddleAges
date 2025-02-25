using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Vector3 Direction { get; protected set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    [SerializeField] private float _destroyAfterSeconds;


    private Coroutine _destroyAfterTimerCoroutine;

    private void OnEnable()
    {
        _destroyAfterTimerCoroutine = StartCoroutine(ReturnToPoolAfterTimer());
    }


    protected virtual void Start()
    {
        //Destroy(gameObject, _destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Environment"))
        {
            IDamagable enemy = other.gameObject.GetComponent<IDamagable>();
            enemy?.TakeDamage(Damage);
        }
    }

    //«¿Ã≈Õ»“‹ Õ¿ UNITASK!!!!!
    private IEnumerator ReturnToPoolAfterTimer()
    {
        float elapsedTime = 0f;

        while(elapsedTime<_destroyAfterSeconds)
        {
            elapsedTime+=Time.deltaTime;
            yield return null;
        }

        PoolManager.ReturnObjectToPool(gameObject);
    }
}
