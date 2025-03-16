using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public GameObject weapon;

    #region weapon_values


    #region updatable 
    [SerializeField]
    private float _damage;
    public float Damage { get; private set; }


    [SerializeField]
    private float _cooldownDuration;
    public float CooldownDuration { get; private set; }


    [SerializeField]
    private float _speed;
    public float Speed { get; private set; }


    [SerializeField]
    private float _duration;
    public float Duration { get; private set; }


    [SerializeField]
    private int _level;
    public int Level { get; private set; }


    [SerializeField]
    private int _repetings;
    public int Repetings { get; private set; }

    #endregion

    [field:SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    #endregion

    private void OnEnable()
    {
        Damage = _damage;
        Speed = _speed;
        CooldownDuration = _duration;
        Duration = _duration;
        Level = _level;
        Repetings = _repetings;
    }


    [SerializeField] private UpgradesScriptableObject _upgradeData;

    public void LevelUpgrade()
    {
        Level++;

        Debug.Log("New level" + Level + " upgrade");

        DamageUpgrade(_upgradeData.DamageMultiplier);


        CooldownUpgrade(_upgradeData.CooldownMultiplier);


        RepetingsUpgrade(_upgradeData.RepetingsAdder);


        DurationUpgrade(_upgradeData.DurationAdder);

        Debug.Log(Level + "  " + Damage + "   " + CooldownDuration + "  " + Repetings + "  " + Duration);
    }


    private void DamageUpgrade(float multiplier)
    {
        Damage *= multiplier;
    }

    private void CooldownUpgrade(float multiplier)
    {
        CooldownDuration *= multiplier;
    }

    private void RepetingsUpgrade(int add)
    {
        Repetings += add;
    }

    private void DurationUpgrade(float add)
    {
        Duration += add;
    }

}
