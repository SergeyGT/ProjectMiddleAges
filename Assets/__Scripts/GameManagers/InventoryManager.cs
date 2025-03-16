using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //public List<WeaponController> _allWeapons = new List<WeaponController>(6);

    [SerializeField] private List<WeaponController> _weaponSlots;

    [SerializeField] private List<Image> _weaponUISlots;

    [SerializeField] private int[] _weaponLevels;

    public int WEAPONS_LIMIT { get; private set; } = 4;


    [System.Serializable]
    private class WeaponUpgrade
    {
        [field:SerializeField] public GameObject InitialWeapon { get; private set; }
        [field: SerializeField] public WeaponScriptableObject weaponData { get; private set; }
    }

    [System.Serializable]
    private class UpgradeUI
    {
        public TextMeshProUGUI upgradeNameDisplay;
        public TextMeshProUGUI upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    [SerializeField]
    private List<WeaponUpgrade> _weaponUpgradeOptions = new List<WeaponUpgrade>();

    [SerializeField]
    private List<UpgradeUI> _uiUpgradeOptions = new List<UpgradeUI>();

    private Player _player;

    private void Awake()
    {        
        _player = GetComponent<Player>();
    }
    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        _weaponSlots.Add(weapon);

        _weaponLevels[slotIndex] = weapon.weaponData.Level;

        _weaponUISlots[slotIndex].enabled = true;
        _weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;


        if (GameManager.Instance!=null && GameManager.Instance.isChoosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if (_weaponSlots.Count >= slotIndex)
        {
            Debug.Log(_weaponSlots.Count + " " + slotIndex);
            WeaponController weapon = _weaponSlots[slotIndex];

            weapon.weaponData.LevelUpgrade();

            if (GameManager.Instance != null && GameManager.Instance.isChoosingUpgrade)
            {
                GameManager.Instance.EndLevelUp();
            }
        }
    }

    private void ApplyUpgradeOptions()
    {
        foreach ( var upgradeOption in _uiUpgradeOptions )
        {
            // Если добавлять пассивки, то рандомом выбирать пассивку или оружие добавляем

            WeaponUpgrade chosenWeaponUpgrade = _weaponUpgradeOptions[Random.Range(0, _weaponUpgradeOptions.Count)];

            bool isNewWeapon = true;
            for (int i =0; i< _weaponSlots.Count && isNewWeapon; i++)
            {
                if (_weaponSlots[i]!=null && _weaponSlots[i].weaponData==chosenWeaponUpgrade.weaponData)
                {
                    isNewWeapon = false;

                    Debug.Log("LevelUp Listener "  + i);
                    upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i-1));

                    upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.name;
                    upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
                }
            }

            if (isNewWeapon)
            {
                Debug.Log("new weapon Listener");
                upgradeOption.upgradeButton.onClick.AddListener( () => _player.SpawnWeapon(chosenWeaponUpgrade.InitialWeapon));

                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.name;
                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
            }

            upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
        }
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
