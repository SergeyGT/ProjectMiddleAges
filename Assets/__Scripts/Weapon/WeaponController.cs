using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{

    public WeaponScriptableObject weaponData;

    protected virtual void Start()
    {
        //weaponData.Level = 1;
    }

    public void LevelUpUpgrade()
    {
        Upgrade();
    }

    protected virtual void Upgrade()
    {
        //weaponData.Level++;
        //if (weaponData.Level <=2) weaponData.Damage *= weaponData.Level;
    }

    protected abstract void Attack();
}
