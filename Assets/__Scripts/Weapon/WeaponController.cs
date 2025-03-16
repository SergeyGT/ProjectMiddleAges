using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{

    public WeaponScriptableObject weaponData;

    protected abstract void Attack();
}
