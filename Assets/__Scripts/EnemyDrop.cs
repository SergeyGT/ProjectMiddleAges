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
    [Header("Set In Inspector")]
    [SerializeField] private List<GameObject> _dropPrefabs;


    public void FallDrop(Vector3 positionDrop, Drop dropColor)
    {

    }

    public void PickDrop()
    {

    }
}
