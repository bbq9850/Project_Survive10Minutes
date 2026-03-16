using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpGrade/UpGradeData")]
public class UpGradeData : ScriptableObject
{
    public string upGradeName;

    public UpGradeType type;

    public float value;

    public WeaponData weaponData;

    public StatType statType;
    
}

public enum UpGradeType
{
    Stat,
    WeaponUnlock,
    WeaponUpgrade
}
public enum StatType
{
    AttackPower,
    AttackSpeed,
    MoveSpeed,
    MaxHp
}
public enum WeaponType
{
    Explosion,
    Kunai
}
