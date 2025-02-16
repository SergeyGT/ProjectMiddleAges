using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public Vector3 Direction {  get; protected set; }

    [SerializeField] private float _destroyAfterSeconds;
    protected virtual void Start()
    {
        Destroy(gameObject, _destroyAfterSeconds);
    }
}
