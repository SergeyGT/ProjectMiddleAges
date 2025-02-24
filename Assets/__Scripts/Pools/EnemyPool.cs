using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [Header("Set In Inspector All Enemy Types")]
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [Range(1,30)][SerializeField] private int _countPrefabs;

    private Dictionary<string, ObjectPool<GameObject>> _pools = new Dictionary<string, ObjectPool<GameObject>>();

    private void Start()
    {
        foreach (var prefab in _enemyPrefabs)
        {
            string key = prefab.name;
            _pools[key] = new ObjectPool<GameObject>(
                () => Instantiate(prefab),
                enemy => enemy.SetActive(true),
                enemy => enemy.SetActive(false),
                enemy => Destroy(enemy), 
                false, 
                _countPrefabs);
        }
    }

    public GameObject GetEnemy(string type)
    {
        return _pools.ContainsKey(type) ? _pools[type].Get() : null;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        if (_pools.ContainsKey(enemy.name))
            _pools[enemy.name].Release(enemy);
    }
}
