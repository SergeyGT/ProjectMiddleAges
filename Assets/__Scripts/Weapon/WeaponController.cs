using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    [SerializeField] protected GameObject _weapon;
    [field: SerializeField] public Sprite Icon { get; private set; }

    private WeaponBehaviour _weaponBehaviour;
    public PlayerMovement PlayerMovement { get; private set; }
    [field:SerializeField]public string Name { get; private set; }
    [field: SerializeField]public string Description { get; private set; }

    public int Level {  get; protected set; }


    protected virtual void Start()
    {
        Level = 1;
        PlayerMovement = FindObjectOfType<PlayerMovement>();
        _weaponBehaviour = _weapon.GetComponent<WeaponBehaviour>();
    }

    public void LevelUpUpgrade()
    {
        Upgrade();
    }

    protected virtual void Upgrade()
    {
        Level++;
        if (Level<=2) _weaponBehaviour.Damage *= Level;
    }

    protected abstract void Attack();
}
