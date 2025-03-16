using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBehaviour : ProjectileWeaponBehaviour
{

    private ScrollController _scrollController;

    private Transform _targetTransform;

    private void OnEnable()
    {
        _scrollController = FindObjectOfType<ScrollController>();
    }

    private void Update()
    {
        if (_targetTransform == null) PoolManager.ReturnObjectToPool(gameObject);
        else transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, weaponData.Speed * Time.deltaTime);
    }


    public void SetTargetTransform(Transform target)
    {
        if (_targetTransform==null)
        {
            _targetTransform = target;
        }
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
