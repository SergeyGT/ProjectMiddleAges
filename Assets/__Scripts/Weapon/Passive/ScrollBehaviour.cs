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
        Debug.Log(_scrollController);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _scrollController.Target.position, Speed * Time.deltaTime);
    }

}
