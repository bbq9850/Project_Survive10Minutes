using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpGrade/UpGradeData")]
public class UpGradeData : ScriptableObject
{
    public string upGradeName;

    public Sprite icon;

    public UpGradeType type;

    public float value;

    public WeaponData weaponData;
}

public enum UpGradeType
{
    AttackPower,
    AttackSpeed,
    MoveSpeed,
    MaxHp,
    Weapon
}
