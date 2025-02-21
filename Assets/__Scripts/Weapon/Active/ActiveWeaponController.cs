using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeaponController : WeaponController
{

    [SerializeField] private LayerMask _groundMask;

    private Vector3 _mousePoint;

    public Vector3 ShootDirection { get; private set; }


    private Camera _cam;
    protected override void Start()
    {
        base.Start();
        _cam = Camera.main;

    }
    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        if (Input.GetMouseButtonDown(0) && GetMousePosition())
        {
            var direction = _mousePoint - transform.position;

            direction.y = 0;
            
            ShootDirection = direction.normalized;

            Attack();
        }
    }

    private bool GetMousePosition()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _groundMask))
        {
            _mousePoint = hitInfo.point;
            return true;
        }
        return false;
    }

    protected override void Attack()
    {
        GameObject go = Instantiate(_weapon);
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.identity;
    }
}
