using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private int _numLevel = 0;

    private bool isSpawning = false;

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
            InvokeRepeating(nameof(SpawnEnemy), 0f, 20f); 
        }
    }

    private void SpawnEnemy()
    {
        EnemySpawner.SetEnemy();
    }
}
