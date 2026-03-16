using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    public Sprite icon;

    public WeaponType weaponType;
    public GameObject weaponPrefab;

    [Header("Level")]
    public WeaponLevelData[] levels;
}
