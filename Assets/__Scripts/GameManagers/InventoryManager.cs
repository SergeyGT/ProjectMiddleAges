using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<WeaponController> _weaponSlots;

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
            // Если добавлять пассивки, то рандомом выбирать пассивку или оружие добавляем

            GameObject chosenWeaponUpgrade = GetRandomUniqueWeapon(addedWeaponOptions);

            WeaponController chosenWeaponController = chosenWeaponUpgrade.GetComponent<WeaponController>();

            Debug.Log(chosenWeaponController.weaponData.name);

            if (_weaponSlots.Contains(chosenWeaponController))
            {
                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(chosenWeaponController));
            }
            else
            {
                upgradeOption.upgradeButton.onClick.AddListener(() => _player.SpawnWeapon(chosenWeaponUpgrade));
            }

            upgradeOption.upgradeNameDisplay.text = chosenWeaponController.weaponData.name;
            upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponController.weaponData.Description;
            upgradeOption.upgradeIcon.sprite = chosenWeaponController.weaponData.Icon;

            addedWeaponOptions.Add(chosenWeaponController);
        }
    }

    private GameObject GetRandomUniqueWeapon(List<WeaponController> lastAddedWeapons)
    {
        GameObject randomUniqueWeapon;
        do
        {
            randomUniqueWeapon = _weaponUpgradeOptions[Random.Range(0, _weaponUpgradeOptions.Count)];

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
