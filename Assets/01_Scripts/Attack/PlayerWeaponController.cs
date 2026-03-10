using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    List<WeaponBase> weapons = new();

    public void AddWeapon(WeaponData data)
    {
        GameObject obj = Instantiate(data.weaponPrefab, transform);

        WeaponBase weapon = obj.GetComponent<WeaponBase>();

        weapons.Add(weapon);
    }
}
