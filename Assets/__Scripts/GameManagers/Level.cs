using UnityEngine;

public class Level : MonoBehaviour
{

    [Range(0,20)][SerializeField] private int _numLevel = 0;
    [SerializeField] private AnimationCurve _skeleton;
    [SerializeField] private AnimationCurve _shooter;
    [SerializeField] private AnimationCurve _ogr;


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
            Debug.LogWarning("Enemy Spawner ��� ����������, ���������� ��������.");
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
    }

    private void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            SpawnEnemy("orc", 10);
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
