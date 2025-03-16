using UnityEngine;
using System;
using System.Collections.Generic;
public class ScrollController : PassiveWeaponController
{

    [SerializeField]private float DETECTION_RADIUS = 10;

    private int DETECT_MILISEC_DELAY = 500;

    [SerializeField] private List<Transform> _detectedEnemies = new List<Transform>();

    private Transform _enemyTarget;

    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();
    }

    private void DetectEnemiesInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, DETECTION_RADIUS);
        _detectedEnemies.Clear();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) 
            {
                _detectedEnemies.Add(hitCollider.gameObject.GetComponent<Transform>());
            }
        }

        if (_detectedEnemies.Count > 0 && !isAttacking)
        {
            GetRandomEnemy();
        }
    }


    private void GetRandomEnemy()
    {
        isAttacking = true;

        List<Transform> copyEnemyList = new List<Transform>(_detectedEnemies);

        Transform randomEnemy;

        if (copyEnemyList.Count!=0)
        {
            do
            {
                randomEnemy = copyEnemyList[UnityEngine.Random.Range(0, copyEnemyList.Count)];
            } while (randomEnemy == null);
        }
    }

    protected override void Attack()
    {
        DetectEnemiesInRange();

        GetRandomEnemy();

        if (_enemyTarget!=null)
        {
            base.Attack();
            PoolManager.SpawnObject(weaponData.weapon, transform, PoolManager.PoolType.Projectiles)
                .GetComponent<ScrollBehaviour>()
                .SetTargetTransform(_enemyTarget);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса обнаружения в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DETECTION_RADIUS);
    }
}
