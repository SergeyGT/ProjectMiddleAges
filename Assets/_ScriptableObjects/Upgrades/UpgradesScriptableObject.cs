using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesScriptableObject", menuName = "ScriptableObjects/Upgrade")]
public class UpgradesScriptableObject : ScriptableObject
{
    #region upgrade_parameters

    [field: SerializeField] public float DamageMultiplier { get; private set; } = 1;

    [field: SerializeField] public float CooldownMultiplier { get; private set; } = 1;

    [field: SerializeField] public int RepetingsAdder { get; private set; } = 0;

    [field: SerializeField] public float DurationAdder { get; private set; } = 0;
    #endregion
}
