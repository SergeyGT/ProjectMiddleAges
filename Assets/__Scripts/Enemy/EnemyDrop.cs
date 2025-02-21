using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Drop
{
    red,
    green, 
    blue
}
public class EnemyDrop : MonoBehaviour
{
    [Tooltip("Расставлять префабы дропа в виде => RGB")]
    [SerializeField] private List<GameObject> _dropPrefabs;

    private List<GameObject> _enemyDrops;
    private Transform _dropAnchor;

    private void Awake()
    {
        _enemyDrops = new List<GameObject>();
        _dropAnchor = GameObject.Find("DropAnchor").transform;
    }
    public void FallDrop(Vector3 positionDrop, Drop dropColor)
    {
        GameObject drop = null;
        switch (dropColor)
        {
            case Drop.red:
                drop = Instantiate(_dropPrefabs[0]);
                break;
            case Drop.green:
                drop = Instantiate(_dropPrefabs[1]);
                break;
            case Drop.blue:
                drop = Instantiate(_dropPrefabs[2]);
                break;
        }        
        drop.transform.position = positionDrop;
        drop.transform.SetParent(_dropAnchor, true);
    }

}
