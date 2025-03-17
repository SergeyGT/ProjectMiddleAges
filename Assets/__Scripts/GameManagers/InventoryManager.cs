using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weaponSlots;

    [SerializeField] private List<Image> _weaponUISlots;

    [SerializeField] private int[] _weaponLevels;

    public int WEAPONS_LIMIT { get; private set; } = 4;

    [System.Serializable]
    private class UpgradeUI
    {
        public TextMeshProUGUI upgradeNameDisplay;
        public TextMeshProUGUI upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    [SerializeField]
    private List<GameObject> _weaponUpgradeOptions = new List<GameObject>();

    [SerializeField]
    private List<UpgradeUI> _uiUpgradeOptions = new List<UpgradeUI>();

    private Player _player;

    private void Awake()
    {        
        _player = GetComponent<Player>();
    }
    public void AddWeapon(int slotIndex, GameObject weapon)
    {
        _weaponSlots.Add(weapon);

        WeaponController weaponController = weapon.GetComponent<WeaponController>();

        _weaponLevels[slotIndex] = weaponController.weaponData.Level;

        _weaponUISlots[slotIndex].enabled = true;
        _weaponUISlots[slotIndex].sprite = weaponController.weaponData.Icon;


        if (GameManager.Instance!=null && GameManager.Instance.isChoosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(WeaponController weaponToUpgrade)
    {

        weaponToUpgrade.weaponData.LevelUpgrade();

        if (GameManager.Instance != null && GameManager.Instance.isChoosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    private void ApplyUpgradeOptions()
    {
        List<WeaponController> addedWeaponOptions = new List<WeaponController>();

        foreach ( var upgradeOption in _uiUpgradeOptions )
        {
            // ≈сли добавл€ть пассивки, то рандомом выбирать пассивку или оружие добавл€ем

            List<GameObject> weaponCollection = _weaponSlots.Count == WEAPONS_LIMIT? _weaponSlots : _weaponUpgradeOptions;

            GameObject modifiedWeaponObject = GetRandomUniqueWeapon(weaponCollection, addedWeaponOptions);

            WeaponController modifiedWeaponController = modifiedWeaponObject.GetComponent<WeaponController>();

            Debug.Log(modifiedWeaponController.weaponData.name);

            if (_weaponSlots.Contains(modifiedWeaponObject))
            {
                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(modifiedWeaponController));
            }
            else
            {
                upgradeOption.upgradeButton.onClick.AddListener(() => _player.SpawnWeapon(modifiedWeaponObject));
            }
            
            upgradeOption.upgradeNameDisplay.text = $"{modifiedWeaponController.weaponData.name} ({modifiedWeaponController.weaponData.Level+1} ”ровень)";
            upgradeOption.upgradeDescriptionDisplay.text = modifiedWeaponController.weaponData.Description;
            upgradeOption.upgradeIcon.sprite = modifiedWeaponController.weaponData.Icon;

            addedWeaponOptions.Add(modifiedWeaponController);
        }
    }

    private GameObject GetRandomUniqueWeapon(List<GameObject> weaponColletion, List<WeaponController> lastAddedWeapons)
    {
        GameObject randomUniqueWeapon;
        do
        {
            randomUniqueWeapon = weaponColletion[UnityEngine.Random.Range(0, weaponColletion.Count)];

        }while(lastAddedWeapons.Contains(randomUniqueWeapon.GetComponent<WeaponController>()));

        if (randomUniqueWeapon==null)
        {
            Debug.LogError("RANDOM UNIQUE SWEAPON CANT BE NULL!!!!");
        }

        return randomUniqueWeapon;
    }


    private void RemoveUpgradeOptions()
    {
        foreach (var upgradeOption in _uiUpgradeOptions)
        {
            upgradeOption.upgradeButton?.onClick.RemoveAllListeners();
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

}
