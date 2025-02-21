using UnityEngine;
public class BlueDiamond : Diamonds
{
    [Header("Set XP")]
    [Range(1,50)][SerializeField] private int _blueDiamondXP;

    protected override void Interact()
    {
        XPBar.Instance.AddExp(_blueDiamondXP);
    }
}
