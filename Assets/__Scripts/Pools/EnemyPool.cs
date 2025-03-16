using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [Header("Set In Inspector All Enemy Types")]
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [Range(1, 30)][SerializeField] private int _countPrefabs;

    private Dictionary<string, ObjectPool<GameObject>> _pools = new Dictionary<string, ObjectPool<GameObject>>();

    private void Awake()
    {
        foreach (var prefab in _enemyPrefabs)
        {
            string key = prefab.name;

            _pools[key] = new ObjectPool<GameObject>(
                () =>
                {
                    GameObject obj = Instantiate(prefab);
                    obj.name = key;
                    obj.SetActive(false); 
                    return obj;
                },
                enemy =>
                {
                    enemy.SetActive(true); 
                },
                enemy =>
                {
                    enemy.SetActive(false);
                },
                enemy =>
                {
                    Destroy(enemy);
                },
                false,
                _countPrefabs
            );

  
            for (int i = 0; i < _countPrefabs; i++)
            {
                GameObject enemy = _pools[key].Get();
                _pools[key].Release(enemy); 
            }
        }
    }

    public GameObject GetEnemy(string type)
    {
        Debug.Log(type);
        if (!_pools.ContainsKey(type))
        {
            Debug.LogError($"No pool found for enemy type: {type}");
            return null;
        }

        GameObject enemy = _pools[type].Get();
        if (enemy == null)
        {
            Debug.LogError($"Pool for {type} is empty!");
        }

        return enemy;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        string key = enemy.name;
        if (_pools.ContainsKey(key))
        {
            _pools[key].Release(enemy);
        }
        else
        {
            Debug.LogError($"Trying to release unknown enemy: {key}");
            Destroy(enemy);
        }
    }
}
