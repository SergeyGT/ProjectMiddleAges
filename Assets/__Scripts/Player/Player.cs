using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxHp;
    private int _currentHp;
    public event Action<int, int> OnHealthChanged;

    private void Start()
    {
        _currentHp = _maxHp;
        OnHealthChanged?.Invoke(_currentHp, _maxHp);
    }

    public void TakeDamage(int damage)
    {
        _currentHp = Mathf.Max(0, _currentHp - damage);
        OnHealthChanged?.Invoke(_currentHp, _maxHp);
        if (_currentHp == 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        CheckDrop();
    }

    private void CheckDrop()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f);
        foreach(Collider collider in colliders)
        {
            IInteract interact = collider.gameObject.GetComponent<IInteract>();
            if(interact != null)
            {
                interact.Interact();
            }
        }
    }
}
