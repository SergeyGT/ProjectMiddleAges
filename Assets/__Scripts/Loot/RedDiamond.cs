using UnityEngine;

public class RedDiamond : Diamonds
{
    [Header("Set XP")]
    [Range(1, 50)][SerializeField] private int _redDiamondXP;
    protected override void Interact()
    {
        XPBar.Instance.AddExp(_redDiamondXP);
    }
}
