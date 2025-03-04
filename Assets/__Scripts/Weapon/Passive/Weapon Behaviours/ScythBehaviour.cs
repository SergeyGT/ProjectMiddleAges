using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScythBehaviour : MeleeWeaponBehaviour
{
    [SerializeField] private int _repetings = 2;


    public void MakeAttack()
    {
        float splashDuration = (float)(DestroyAfterSeconds / (_repetings * 2));
        transform.DORotate(new Vector3(0,360, 0), 5, RotateMode.LocalAxisAdd)
            .SetLoops(_repetings)
            .SetEase(Ease.Linear);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
