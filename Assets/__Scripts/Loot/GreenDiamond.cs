using UnityEngine;

public class GreenDiamond : Diamonds
{
    [Header("Set XP")]
    [Range(1, 50)][SerializeField] private int _greenDiamondXP;
    protected override void Interact()
    {
        XPBar.Instance.AddExp(_greenDiamondXP);
    }
}
