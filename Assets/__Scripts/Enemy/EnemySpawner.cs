using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private Vector2 _rangeXPos = Vector2.zero;
    [SerializeField] private Vector2 _rangeYPos = Vector2.zero;
    [SerializeField] private Vector2 _rangeZPos = Vector2.zero;
    [SerializeField] private int _enemyCount = 0;

    [Header("Set Dynamically")]
    private List<GameObject> _enemies;
    private Transform _enemiesAnchor;
    private Transform _playerTransform;
    private IDamagable _playerDamagable;

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
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
            _playerDamagable = player.GetComponent<IDamagable>();
        }
    }

    //HACK: добавить логику генерации врага в класс, управляющий игрой
    static public void SetEnemy(Enemy s = null)
    {    
        for (int i = 0; i < ES._enemyCount; i++)
        {
            GameObject enemy = Instantiate(ES._enemyPrefabs[0]);
            ES._enemies.Add(enemy);
            enemy.transform.SetParent(ES._enemiesAnchor, false);

            Enemy en = enemy.GetComponent<Enemy>();
            if (en != null)
            {
                en.Init(ES._playerTransform, ES._playerDamagable);
            }

            Vector3 offset = Random.insideUnitSphere;
            offset.x *= Random.Range(ES._rangeXPos.x, ES._rangeXPos.y);
            offset.y *= Random.Range(ES._rangeYPos.x, ES._rangeYPos.y);
            offset.z *= Random.Range(ES._rangeZPos.x, ES._rangeZPos.y);

            enemy.transform.position= offset;
            enemy.SetActive(true);

        }
    }

}
