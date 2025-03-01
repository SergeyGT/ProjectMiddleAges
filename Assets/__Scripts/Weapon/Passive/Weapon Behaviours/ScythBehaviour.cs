using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScythBehaviour : MeleeWeaponBehaviour
{
    [SerializeField] private int _repetings = 2;

    [SerializeField] private float _rotateDuration = 3f;

    private Sequence _attackAnim;

    protected override void Start()
    {
        base.Start();
    }

    public void StartAnim()
    {
        transform.DORotate(transform.position, _rotateDuration, RotateMode.FastBeyond360).SetLoops(_repetings);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
