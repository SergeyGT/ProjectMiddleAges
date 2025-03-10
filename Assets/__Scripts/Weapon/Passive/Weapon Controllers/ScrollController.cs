using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
public class ScrollController : PassiveWeaponController
{

    [SerializeField]private float _detectionRadius = 10;

    private int _detectMilisecDelay = 500;

    private CancellationTokenSource _cancellationTokenSource;

    [SerializeField] private List<Transform> _detectedEnemies = new List<Transform>();

    private Transform _enemyTarget;

    private bool isAttacking = false;

    private void OnEnable()
    {
        _cancellationTokenSource = new CancellationTokenSource();
    }

    protected override void Start()
    {
        base.Start();
        DetectEnemies(_cancellationTokenSource.Token).Forget();
    }

    private async UniTaskVoid DetectEnemies(CancellationToken cancellationToken)
    {
        try
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested(); // ��������, ���� �������� ���� ���������

                DetectEnemiesInRange();
                await UniTask.Delay(_detectMilisecDelay, cancellationToken: cancellationToken); // �������� �� �������� �������
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("�������� ���������");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"������: {ex.Message}");
        }
    }

    private void DetectEnemiesInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius);
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
            AttackEnemies();
        }
    }


    private async void AttackEnemies()
    {
        isAttacking = true;

        List<Transform> copyEnemyList = new List<Transform>(_detectedEnemies);

        foreach (Transform enemy in copyEnemyList)
        {
            if (enemy == null) continue; // �������, ���� ��� ���� ��� ������
            try
            {
                _enemyTarget = enemy;
                Attack();
                await UniTask.Delay((int)(CooldownDuration * 1000)); // �������� ����� �������
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"������ ��� ����� ����: {ex.Message}");
            }
        }

        isAttacking = false;
    }

    protected override void Attack()
    {
        if (_enemyTarget!=null)
        {
            base.Attack();
            PoolManager.SpawnObject(_weapon, transform, PoolManager.PoolType.Projectiles)
                .GetComponent<ScrollBehaviour>()
                .SetTargetTransform(_enemyTarget);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ������������ ������� ����������� � ���������
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }


    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
