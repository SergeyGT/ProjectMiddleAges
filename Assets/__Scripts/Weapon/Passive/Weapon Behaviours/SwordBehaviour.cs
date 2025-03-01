using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MeleeWeaponBehaviour
{
    [SerializeField] private float _maxAngle = 10;
    
    [SerializeField] private int _repetings = 2;

    private float _rotateDuration = 0.5f;

    private Vector3 _eulerRotation;

    private Sequence _attackAnim;

    protected override void Start()
    {
        base.Start();
        SetEulerVector();

        Debug.Log("Rotation " + transform.rotation + " " + gameObject.name);

        _attackAnim = DOTween.Sequence()
            .Append(transform.DORotate(transform.up*90, _rotateDuration))
            .Append(transform.DORotate(-transform.up*90 , _rotateDuration))
            .SetLoops(_repetings)
            .SetAutoKill(false);
    }

    public void StartAnim()
    {
        Debug.Log("Anim started");

        _attackAnim.Restart();
    }

    public void SetEulerVector()
    {
        _eulerRotation = new Vector3(0, _maxAngle, 0);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
