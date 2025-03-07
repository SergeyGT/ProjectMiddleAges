using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    [SerializeField] private AnimationCurve _skeleton;
    [SerializeField] private AnimationCurve _shooter;
    [SerializeField] private AnimationCurve _orc;

    private Dictionary<string,int> GenerateWave(int currentLevel)
    {
        return new Dictionary<string, int>
        {
            { "Skeleton", Mathf.RoundToInt(_skeleton.Evaluate(currentLevel)) },
            { "Shooter", Mathf.RoundToInt(_shooter.Evaluate(currentLevel)) },
            { "Orc", Mathf.RoundToInt(_orc.Evaluate(currentLevel)) }
        };
    }
}
