using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : MonoBehaviour
{
    public event Action<int> XpChanged;

    protected void ChangeHp(int xp)
    {
        XpChanged?.Invoke(xp);
    }
}
