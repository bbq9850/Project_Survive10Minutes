using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float attackPower = 10;
    public float attackSpeed = 1;
    public float moveSpeed = 5;
    public float maxHP = 100;

    public event Action<float> OnMaxHpChanged;
    //public event Action<float> OnAttackPowerChanged;

    public void ApplyUpgrade(UpGradeData option)
    {
        switch (option.type)
        {
            case UpGradeType.AttackPower:
                attackPower += option.value;
                //OnAttackPowerChanged?.Invoke(attackPower);
                break;

            case UpGradeType.AttackSpeed:
                attackSpeed += option.value;
                break;

            case UpGradeType.MoveSpeed:
                moveSpeed += option.value;
                break;

            case UpGradeType.MaxHp:
                maxHP += option.value;
                OnMaxHpChanged?.Invoke(maxHP);
                break;

        }
    }
}
