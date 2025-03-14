using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(InventoryManager))]
public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxHp;
    [Header("Percentage For Up Max Hp")]
    [Range(1,100)][SerializeField] private int _percentageUpgradeHp;

    private InventoryManager _inventory;
    private int _weaponIndex;

    private int _currentHp;

    // �������� ��� ����� ����������
    public int CurrentHp
    {
        get { return _currentHp; }
        private set
        {
            if (_currentHp != value)
            {
                _currentHp = value;
                if (GameManager.Instance!=null)
                {
                    GameManager.Instance.currentHealthDisplay.text = "Health: " + _currentHp;
                }
            }
        }
    }

    public event Action<int, int> OnHealthChanged;

    private void Awake()
    {
        _inventory = GetComponent<InventoryManager>();
    }


    private void Start()
    {
        CurrentHp = _maxHp;
        OnHealthChanged?.Invoke(CurrentHp, _maxHp);
    }

    public void TakeDamage(int damage)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - damage);
        OnHealthChanged?.Invoke(CurrentHp, _maxHp);
        if (CurrentHp == 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.AssignLevelReachedUI(Level.L.numL);
            GameManager.Instance.GameOver();
        }
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

    private void OnEnable()
    {
        Level.UpgradeStats += ChangeMaxHp;
    }

    private void OnDisable()
    {
        Level.UpgradeStats -= ChangeMaxHp;
    }

    private void ChangeMaxHp()
    {
        CurrentHp += CurrentHp * _percentageUpgradeHp / 100;
        OnHealthChanged?.Invoke(Mathf.RoundToInt(CurrentHp), _maxHp);
    }

    private void SpawnWeapon(GameObject weapon)
    {
        if (_weaponIndex >= (_inventory.WEAPONS_LIMIT-1))
        {
            Debug.LogError("Inventory is already full");
            return;
        }

        weapon.SetActive(true);
        _inventory.AddWeapon(_weaponIndex, weapon.GetComponent<WeaponController>());
        _weaponIndex++;
    }
}
