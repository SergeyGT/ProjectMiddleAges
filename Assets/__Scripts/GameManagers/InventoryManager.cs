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

    public int WEAPONS_LIMIT {  get; private set; }


    [System.Serializable]
    private class WeaponUpgrade
    {
        [field:SerializeField] public GameObject InitialWeapon { get; private set; }
        [field: SerializeField] public WeaponController WeaponController { get; private set; }
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
        WEAPONS_LIMIT = 3;
        
        _weaponSlots = new List<WeaponController>(WEAPONS_LIMIT);
        _weaponUISlots = new List<Image>(WEAPONS_LIMIT);
        _weaponLevels = new int[WEAPONS_LIMIT];

        _player = GetComponent<Player>();
    }
    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        _weaponSlots[slotIndex] = weapon;
        _weaponLevels[slotIndex] = weapon.Level;
        _weaponUISlots[slotIndex].enabled = true;
        _weaponUISlots[slotIndex].sprite = weapon.Icon;


        if (GameManager.Instance!=null && GameManager.Instance.isChoosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if (_weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = _weaponSlots[slotIndex];
            weapon.LevelUpUpgrade();

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
                if (_weaponSlots[i]!=null && _weaponSlots[i]==chosenWeaponUpgrade.WeaponController)
                {
                    isNewWeapon = false;
                    upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i));

                    upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.WeaponController.Name;
                    upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.WeaponController.Description;
                }
            }

            if (isNewWeapon)
            {
                upgradeOption.upgradeButton.onClick.AddListener( () => _player.SpawnWeapon(chosenWeaponUpgrade.InitialWeapon));

                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.WeaponController.Name;
                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.WeaponController.Description;
            }

            upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.WeaponController.Icon;
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
