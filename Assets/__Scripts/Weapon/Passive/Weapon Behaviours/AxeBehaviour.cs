using DG.Tweening;
using UnityEngine;
public class AxeBehaviour : ProjectileWeaponBehaviour
{

    //����� ������������ �������� �� �������� ������, ����� duration ��������� ����� Speed

    [SerializeField] private float _highY = 20;
    [SerializeField] private float _maxX = 10;
    [SerializeField] private float _maxZ = 10;


    [SerializeField] private float _goingUpDuration = 1f;
    [SerializeField] private float _goingDownDuration = 1f;

    private float _underGroundY = -5;
    protected override void Start()
    {
        base.Start();
    }


    public void MakeAnimSequence()
    {
        Vector3 fallSpot = new Vector3(Random.Range(-_maxX, _maxX), _highY, Random.Range(-_maxZ, _maxZ));


        DOTween.Sequence()
            .Append(transform.DOMoveY(_highY, 1))
            .Append(transform.DOMove(fallSpot, 1f))
            .AppendInterval(2)
            .Append(transform.DORotate(new Vector3(90, 0, 0), 1))
            .Append(transform.DOMoveY(_underGroundY, 1).SetEase(Ease.InQuint));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
