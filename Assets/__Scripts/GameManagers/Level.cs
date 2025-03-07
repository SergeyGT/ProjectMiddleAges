using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [Range(0,20)][SerializeField] private int _numLevel = 0;

    private Wave _wave;
    private bool isSpawning = false;
    public static Level L;

    public static Action UpgradeStats;

    public int numL
    {
        get
        {
            return _numLevel;
        }
        set
        {
            _numLevel = value;
            LevelUp();
        }

    }

    private void Awake()
    {
        if (L != null && L != this)
        {
            Debug.LogWarning("Enemy Spawner ��� ����������, ���������� ��������.");
            Destroy(gameObject);
            return;
        }

        L = this;
    }

    private void Start()
    {
        _wave = GetComponent<Wave>();
        StartSpawning();
    }

    public void LevelUp()
    {
        UpgradeStats?.Invoke();
        StartSpawning();
    }

    private void StartSpawning()
    {
        Dictionary<string, int> _waveData = _wave.GenerateWave(_numLevel);
        foreach(var enemy in  _waveData)
        {
            SpawnEnemy(enemy.Key, enemy.Value);
        }
    }

    /// <summary>
    /// ����� ������ ������.
    /// </summary>
    /// <param name="nameEnemy">��� ����� (��. ��� �������).</param>
    /// <param name="countEnemies">���������� ������, ������� ���������� ����������.</param>
    private void SpawnEnemy(string nameEnemy, int countEnemies)
    {
        EnemySpawner.SetEnemy(nameEnemy, countEnemies);
    }
}
