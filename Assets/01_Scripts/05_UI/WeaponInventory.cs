using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] Transform slotParent;
    [SerializeField] WeaponSlotUI slotPrefab;

    List<WeaponSlotUI> slots = new();

    public void AddWeapon(WeaponBase weapon)
    {
        WeaponSlotUI slot =
            Instantiate(slotPrefab, slotParent);

        slot.Setup(weapon);

        slots.Add(slot);
    }

    public void Refresh()
    {
        foreach (var slot in slots)
        {
            slot.UpdateUI();
        }
    }
}
