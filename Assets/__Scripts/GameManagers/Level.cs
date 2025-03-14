using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            if (GameManager.Instance!=null)
            {
                GameManager.Instance.currentLevelDisplay.text = "Level: " + _numLevel;
            }
            LevelUp();
        }

    }

    private void Awake()
    {
        if (L != null && L != this)
        {
            Debug.LogWarning("Enemy Spawner уже существует, уничтожаем дубликат.");
            Destroy(gameObject);
            return;
        }

        L = this;
        if (GameManager.Instance!=null)
        {
            GameManager.Instance.currentLevelDisplay.text = "Level: " + _numLevel;
        }
    }

    private void Start()
    {
        _wave = GetComponent<Wave>();
        StartSpawning();
    }

    public void LevelUp()
    {
        UpgradeStats?.Invoke();
        GameManager.Instance.StartLevelUp();
        //StartSpawning();
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
    /// Метод спавна врагов.
    /// </summary>
    /// <param name="nameEnemy">Имя врага (см. имя префаба).</param>
    /// <param name="countEnemies">Количество врагов, которое необходимо заспавнить.</param>
    private void SpawnEnemy(string nameEnemy, int countEnemies)
    {
        EnemySpawner.SetEnemy(nameEnemy, countEnemies);
    }
}
