using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private Vector2 _rangeXPos = Vector2.zero;
    [SerializeField] private Vector2 _rangeZPos = Vector2.zero;

    [Header("Set Dynamically")]
    private List<GameObject> _enemies;

    private EnemySpawner ES;

    private void Awake()
    {
        ES = this;
        _enemies = new List<GameObject>();
    }

    //HACK: �������� ������ ��������� ����� � �����, ����������� �����
    static private void SetEnemy(Enemy s)
    {

    }


}
