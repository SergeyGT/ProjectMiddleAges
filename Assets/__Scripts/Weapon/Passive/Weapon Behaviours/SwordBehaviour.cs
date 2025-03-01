using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MeleeWeaponBehaviour
{   
    [SerializeField] private int _repetings = 2;

    private float _rotateDuration = 0.5f;

    private Sequence _attackAnim;

    protected override void Start()
    {
        base.Start();
     

        Debug.Log("Rotation " + transform.rotation + " " + gameObject.name);

        _attackAnim = DOTween.Sequence()
            .Append(transform.DORotate(transform.up*90, _rotateDuration))
            .Append(transform.DORotate(-transform.up*90 , _rotateDuration))
            .SetLoops(_repetings)
            .SetAutoKill(false);
    }

    public void StartAnim()
    {
        _attackAnim.Restart();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
