using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveProjectileBehaviour : ProjectileWeaponBehaviour
{
    private ActiveWeaponController _controller;

    private Rigidbody _rb;
    protected override void Start()
    {
        base.Start();

        _rb = GetComponent<Rigidbody>();    

        _controller = FindObjectOfType<ActiveWeaponController>();
        Direction = _controller.ShootDirection * Speed;
    }


    protected void Update()
    {
        transform.position += Direction * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(Direction);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}

