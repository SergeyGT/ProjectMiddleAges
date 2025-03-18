<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemySpawner : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private Vector2 _rangeXPos = Vector2.zero;
    [SerializeField] private Vector2 _rangeYPos = Vector2.zero;
    [SerializeField] private Vector2 _rangeZPos = Vector2.zero;

    [Header("Set Dynamically")]

    [Header("NavMesh Settings")]
    [SerializeField] private float _spawnRadius = 1.0f;         
    [SerializeField] private int _maxAttempts = 5;            

    private List<GameObject> _enemies;
    private Transform _enemiesAnchor;
    private Transform _playerTransform;
    private IDamagable _playerDamagable;
    private EnemyPool _enemyPool;

    static private EnemySpawner ES;

    



    private void Awake()
    {
        if (ES != null && ES != this)
        {
            Debug.LogWarning("Enemy Spawner уже существует, уничтожаем дубликат.");
            Destroy(gameObject);
            return;
        }

        ES = this;
        _enemies = new List<GameObject>();
        _enemiesAnchor = GameObject.Find("EnemiesAnchor").transform;

        _enemyPool = FindObjectOfType<EnemyPool>();
        if (_enemyPool == null)
        {
            Debug.LogError("EnemyPool is null!");
        }



        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
            _playerDamagable = player.GetComponent<IDamagable>();
        }

        //DontDestroyOnLoad(gameObject);
    }




    //HACK: добавить логику генерации врага в класс, управляющий игрой

        DontDestroyOnLoad(gameObject);
    }

    static public void SetEnemy(string enemyType, int countEnemies)
    {
        if (ES._enemyPool == null)
        {
            Debug.LogError("EnemyPool is null!");
            return;
        }



        for (int i = 0; i < countEnemies; i++)
        {
            GameObject enemy = ES._enemyPool.GetEnemy(enemyType);
            if (enemy == null)
            {
                Debug.LogError("Enemy is Null");
                continue;
            }

            ES._enemies.Add(enemy);
            enemy.transform.SetParent(ES._enemiesAnchor, false);

            Enemy en = enemy.GetComponent<Enemy>();
            if (en != null)
            {
                en.Init(ES._playerTransform, ES._playerDamagable);
            }

            Vector3 offset = new Vector3(
                Random.Range(ES._rangeXPos.x, ES._rangeXPos.y),
                Random.Range(ES._rangeYPos.x, ES._rangeYPos.y),
                Random.Range(ES._rangeZPos.x, ES._rangeZPos.y)
            );

            enemy.transform.position = offset;

            Vector3 spawnPos = ES.GetValidSpawnPosition();

            enemy.transform.position = spawnPos;

            enemy.SetActive(true);
        }
    }


    private Vector3 GetValidSpawnPosition()
    {
        for (int attempt = 0; attempt < _maxAttempts; attempt++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(_rangeXPos.x, _rangeXPos.y),
                Random.Range(_rangeYPos.x, _rangeYPos.y),
                Random.Range(_rangeZPos.x, _rangeZPos.y)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, _spawnRadius, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return new Vector3(
            (_rangeXPos.x + _rangeXPos.y) / 2,
            (_rangeYPos.x + _rangeYPos.y) / 2,
            (_rangeZPos.x + _rangeZPos.y) / 2
        );
    }

    static public void RemoveEnemy(GameObject enemy)
    {
        if (ES._enemyPool != null)
        {
            enemy.SetActive(false);
            ES._enemyPool.ReleaseEnemy(enemy);
        }
    }


}
