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

    [SerializeField] private GameObject _activeWeapon;
    [SerializeField] private AudioClip _hit;

    private AudioSource _source;

    private InventoryManager _inventory;
    private int _weaponIndex;

    private int _currentHp;
    private Animator _animator;

    // Свойство для вывод статистики
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


        _weaponIndex = 0;
        SpawnWeapon(_activeWeapon);

        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();

    }

    public void TakeDamage(int damage)
    {
        SoundHitActivate();
        CurrentHp = Mathf.Max(0, CurrentHp - damage);
        OnHealthChanged?.Invoke(CurrentHp, _maxHp);
        if (CurrentHp == 0)
        {
            Kill();
        }
    }

    private void SoundHitActivate()
    {
        if(!_source.isPlaying)
        {
            SoundManager.Instance.PlayLocalSound(_source, _hit);
        }
    }
    private void Kill()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.AssignLevelReachedUI(Level.L.numL);
            GameManager.Instance.AssignChosenWeaponsToUI(_inventory.GetCurrentWeaponUISlots());
            GameManager.Instance.GameOver();
        }
        Destroy(this.gameObject);
    }

    protected IEnumerator DelayDeath()
    {
        _animator.SetBool("Death", true);
        yield return new WaitForSeconds(3f);
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
        print(CurrentHp);
        OnHealthChanged?.Invoke(Mathf.RoundToInt(CurrentHp), _maxHp);
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (_weaponIndex > (_inventory.WEAPONS_LIMIT-1))
        {
            Debug.Log("Inventory is already full");
            return;
        }

        weapon.SetActive(true);
        _inventory.AddWeapon(_weaponIndex, weapon);
        _weaponIndex++;
    }
}
