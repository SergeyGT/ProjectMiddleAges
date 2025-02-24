using UnityEngine;

public class Level : MonoBehaviour
{

    [Range(0,20)][SerializeField] private int _numLevel = 0;

    private bool isSpawning = false;
    public static Level L;

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
        StartSpawning();
        print(_numLevel);
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
        EnemySpawner.SetEnemy("Skeleton");
    }
}
