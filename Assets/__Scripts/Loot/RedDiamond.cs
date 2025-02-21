using UnityEngine;

public class RedDiamond : Diamonds, IInteract
{
    [Header("Set XP")]
    [Range(1, 50)][SerializeField] private int _redDiamondXP;
    public void Interact()
    {
        XPBar.Instance.AddExp(_redDiamondXP);
        Destroy(this.gameObject);
    }
}
