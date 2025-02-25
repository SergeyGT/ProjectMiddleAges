using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBehaviour : ProjectileWeaponBehaviour
{

    [SerializeField] private float _highY = 30;
    [SerializeField] private float _maxX = 30;
    [SerializeField] private float _maxZ = 30;

    protected override void Start()
    {
        base.Start();


        Vector3 fallSpot = new Vector3(Random.Range(-_maxX, _maxX), _highY, Random.Range(-_maxZ, _maxZ));



    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
