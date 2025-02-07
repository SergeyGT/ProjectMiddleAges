using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private Vector2 _rangeXPos = Vector2.zero;
    [SerializeField] private Vector2 _rangeZPos = Vector2.zero;

    private EnemySpawner ES;
    private List<GameObject> _enemies;

    private void Awake()
    {
        ES = this;
        _enemies = new List<GameObject>();
    }

    //HACK: добавить логику генерации врага в класс, управляющий игрой
    static private void SetEnemy(Enemy s)
    {

    }


}
