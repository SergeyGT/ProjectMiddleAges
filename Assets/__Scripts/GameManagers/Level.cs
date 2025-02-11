using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private int _numLevel = 0;

    private bool isSpawning = false;
    private Level L;

    private void Awake()
    {
        if (L != null && L != this)
        {
            Debug.LogWarning("Enemy Spawner уже существует, уничтожаем дубликат.");
            Destroy(gameObject);
            return;
        }

        L = this;
    }

    private void Start()
    {
        StartSpawning();
    }

    public void LevelUp()
    {
        _numLevel++;
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        EnemySpawner.SetEnemy();
    }
}
