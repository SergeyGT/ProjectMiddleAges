using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    private KnifeController _knifeController;
    protected override void Start()
    {
        base.Start();

        _knifeController = FindObjectOfType<KnifeController>();

        //transform.position = _knifeController.transform.position;
        Direction = _knifeController.PlayerMovement.LastMovedVector.normalized * Speed;
        transform.rotation = Quaternion.LookRotation(Direction);
    }

    protected void Update()
    {
        transform.position += Direction * Time.deltaTime;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
