using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBehaviour : ProjectileWeaponBehaviour
{

    private ScrollController _scrollController;

    private void OnEnable()
    {
        _scrollController = FindObjectOfType<ScrollController>();
    }

    private void Update()
    {
        if (_scrollController.Target == null) PoolManager.ReturnObjectToPool(gameObject);
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _scrollController.Target.position, Speed * Time.deltaTime);
        }
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
