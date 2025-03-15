using System.Collections;
using System.Collections.Generic;
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


    private void Awake()
    {
        WEAPONS_LIMIT = 3;
        
        _weaponSlots = new List<WeaponController>(WEAPONS_LIMIT);
        _weaponUISlots = new List<Image>(WEAPONS_LIMIT);
        _weaponLevels = new int[WEAPONS_LIMIT];
    }
    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        _weaponSlots[slotIndex] = weapon;
        _weaponLevels[slotIndex] = weapon.Level;
        _weaponUISlots[slotIndex].enabled = true;
        _weaponUISlots[slotIndex].sprite = weapon.Icon;
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if (_weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = _weaponSlots[slotIndex];
            weapon.LevelUpUpgrade();
        }
    }
}
