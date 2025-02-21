using UnityEngine;
public class BlueDiamond : Diamonds, IInteract
{
    [Header("Set XP")]
    [Range(1,50)][SerializeField] private int _blueDiamondXP;

    public void Interact()
    {
        base.ChangeHp(_blueDiamondXP);
        Destroy(this.gameObject);
    }
}
