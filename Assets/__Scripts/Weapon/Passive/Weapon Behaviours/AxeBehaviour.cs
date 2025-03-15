using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
public class AxeBehaviour : MeleeWeaponBehaviour
{

    //По семантике методов AxeBehaviour больше подходит для классов ближнего боя

    [SerializeField] private float Max_X_Range = 10;
    
    [SerializeField] private float max_Z_Range = 10;
    
    [SerializeField] private float TOP_Y_VALUE = 20;

    private float UNDER_GROUND_VALUE = -5;

    public override void MakeAttack()
    {
        Vector3 fallSpot = new Vector3(Random.Range(-Max_X_Range, Max_X_Range), TOP_Y_VALUE, Random.Range(-max_Z_Range, max_Z_Range));

        float goingUpDuration = ANIM_DURATION * 0.3f;

        float rotationDuration = ANIM_DURATION * 0.1f;
        
        float goingToSpotDuration = ANIM_DURATION * 0.1f;

        float intervalDuration = ANIM_DURATION * 0.3f;
        
        float fallDuration = ANIM_DURATION * 0.2f;

        DOTween.Sequence()
            .Append(transform.DOMoveY(TOP_Y_VALUE, goingUpDuration))
            .Append(transform.DORotate(new Vector3(90, 0, 0), rotationDuration))
            .Append(transform.DOMove(fallSpot, goingToSpotDuration))
            .AppendInterval(intervalDuration)
            .Append(transform.DOMoveY(UNDER_GROUND_VALUE, fallDuration)
            .SetEase(Ease.InQuint))
            .OnComplete(() => PoolManager.ReturnObjectToPool(gameObject));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
