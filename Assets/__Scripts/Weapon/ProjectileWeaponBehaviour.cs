using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class ProjectileWeaponBehaviour : WeaponBehaviour
{
    [field: SerializeField] public float Speed { get; private set; }

    [SerializeField] private float projectileDuration = 5;

    private CancellationTokenSource _cancellationTokenSource;

    private void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        ReturnToPoolAfterTimer(_cancellationTokenSource.Token).Forget();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Environment"))
        {
            PoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void OnDisable()
    {
        // �������� ������ ��� ���������� �������
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null; // ������� ������ ����� �����������
        }
    }

    private async UniTaskVoid ReturnToPoolAfterTimer(CancellationToken cancellationToken)
    {
        try
        {
            await UniTask.Delay((int)(projectileDuration * 1000), cancellationToken: cancellationToken);
            PoolManager.ReturnObjectToPool(gameObject);
        }
        catch (OperationCanceledException)
        {
            // �������� ������, ���� ��� �����
            Debug.LogWarning("������ ��������� � ��� �� ���������� ��������.");
        }
        catch (Exception ex)
        {
            // �������� ����� ������ ����������, ����� ������� ������������� ������
            Debug.LogError("������ ��� ����������� ������� � ���: " + ex.Message);
        }
    }
}