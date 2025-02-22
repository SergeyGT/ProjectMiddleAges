using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : MonoBehaviour, IInteract
{
    [HideInInspector]
    public event Action<int> XpChanged;
    public static event Action<Diamonds> OnDiamondSpawned;

    [Range(1,50)]
    [SerializeField] private int _xp;


    private void Awake()
    {
        OnDiamondSpawned?.Invoke(this);
    }
    public void Interact()
    {
        XpChanged?.Invoke(_xp);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        XpChanged = null; 
    }

}
