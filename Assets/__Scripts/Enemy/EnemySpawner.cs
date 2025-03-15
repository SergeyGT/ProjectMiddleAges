using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private Vector2 _rangeXPos = Vector2.zero;
    [SerializeField] private Vector2 _rangeYPos = Vector2.zero;
    [SerializeField] private Vector2 _rangeZPos = Vector2.zero;

    [Header("Set Dynamically")]
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
        _enemiesAnchor = GameObject.Find("EnemiesAnchor").GetComponent<Transform>();

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

        DontDestroyOnLoad(gameObject);
    }


    //HACK: добавить логику генерации врага в класс, управляющий игрой
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
            enemy.SetActive(true);
        }
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
