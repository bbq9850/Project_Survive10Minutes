using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    List<WeaponBase> weapons = new();
    [SerializeField] WeaponInventory weaponInventory;

    [SerializeField] PlayerHUD playerHUD;

    [SerializeField] WeaponData basicWeapon;
    PlayerStat stat;

    private void Awake()
    {
        stat = GetComponent<PlayerStat>();
    }
    private void Start()
    {
        AddWeapon(basicWeapon);
    }
    public void AddWeapon(WeaponData data)
    {
        WeaponBase existingWeapon = GetWeapon(data);

        if (existingWeapon != null)
        {
            if (!existingWeapon.IsMaxLevel())
            {
                existingWeapon.LevelUp(1);
                RefreshUI();
            }

            return;
        }


        GameObject obj = Instantiate(data.weaponPrefab, transform);

        WeaponBase weapon = obj.GetComponent<WeaponBase>();

        weapon.Init(data, stat);

        weapons.Add(weapon);

        playerHUD.AddWeaponUI(weapon);
    }

    public bool HasWeapon(WeaponData data)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.Data == data)
                return true;
        }

        return false;
    }

    public WeaponBase GetWeapon(WeaponData data)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.Data == data)
                return weapon;
        }

        return null;
    }

    public void RefreshUI()
    {
        weaponInventory.Refresh();
    }
}
