using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public GameObject weapon;

    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float CooldownDuration { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field:SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field:SerializeField] public int Repetings { get; private set; }


    public void DamageUpgrade(float multiplier)
    {
        Damage *= multiplier;
    }

    public void CooldownUpgrade(float multiplier)
    {
        CooldownDuration *= multiplier;
    }

    public void RepetingsUpgrade(int add)
    {
        Damage += add;
    }

    public void DurationUpgrade(int add)
    {
        Duration += add;
    }

}
