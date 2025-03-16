using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public GameObject weapon;

    [field: SerializeField] public float CooldownDuration { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    [field: SerializeField] public float Duration { get; private set; }
    [field:SerializeField] public Sprite Icon { get; private set; }

    //[field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }

    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public string Description { get; private set; }

    [field: SerializeField] public int Level { get; private set; }


}
