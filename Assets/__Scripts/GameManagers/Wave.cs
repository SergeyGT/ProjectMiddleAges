using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private AnimationCurve _skeleton;
    [SerializeField] private AnimationCurve _shooter;
    [SerializeField] private AnimationCurve _orc;

    public Dictionary<string,int> GenerateWave(int currentLevel)
    {
        return new Dictionary<string, int>
        {
            { "Skeleton", Mathf.RoundToInt(_skeleton.Evaluate(currentLevel)) },
            { "Shooter", Mathf.RoundToInt(_shooter.Evaluate(currentLevel)) },
            { "orc", Mathf.RoundToInt(_orc.Evaluate(currentLevel)) }
        };
    }
}
