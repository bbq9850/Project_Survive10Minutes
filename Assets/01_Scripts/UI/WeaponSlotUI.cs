using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text levelText;

    WeaponBase weapon;

    public void Setup(WeaponBase weapon)
    {
        this.weapon = weapon;

        icon.sprite = weapon.Data.icon;

        UpdateUI();
    }

    public void UpdateUI()
    {
        levelText.text = "Lv " + weapon.Level;
    }
}
