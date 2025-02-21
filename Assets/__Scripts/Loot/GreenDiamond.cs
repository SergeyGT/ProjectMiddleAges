using UnityEngine;

public class GreenDiamond : Diamonds, IInteract
{
    [Header("Set XP")]
    [Range(1, 50)][SerializeField] private int _greenDiamondXP;
    public void Interact()
    {
        XPBar.Instance.AddExp(_greenDiamondXP);
        Destroy(this.gameObject);
    }
}
