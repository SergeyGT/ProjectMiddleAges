using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    [SerializeField] private float _destroyAfterSeconds;
    protected virtual void Start()
    {
        Destroy(gameObject, _destroyAfterSeconds);
    }
}
