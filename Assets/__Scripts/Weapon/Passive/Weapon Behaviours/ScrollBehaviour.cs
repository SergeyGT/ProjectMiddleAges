using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBehaviour : ProjectileWeaponBehaviour
{
    
    [SerializeField]private float ATTACK_HIGH_OFFSET = 0.75f;

    private ScrollController _scrollController;

    private Transform _targetTransform;

    public void SetTargetTransform(Transform target)
    {
        if (_targetTransform==null)
        {
            _targetTransform = target;
        }
    }


    private void Update()
    {
        if (_targetTransform == null) PoolManager.ReturnObjectToPool(gameObject);
        else
        {
            Vector3 targetPosition = new Vector3(_targetTransform.position.x, _targetTransform.position.y + ATTACK_HIGH_OFFSET, _targetTransform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, weaponData.Speed * Time.deltaTime);
            Debug.Log($"ATTACK_HIGH_OFFSET {ATTACK_HIGH_OFFSET}\nTarget position " + targetPosition);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
