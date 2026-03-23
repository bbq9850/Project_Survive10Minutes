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
    public event Action<float> OnAttackPowerChanged;
    public event Action<float> OnAttackSpeedChanged;
    public event Action<float> OnMoveSpeedChanged;

    void Start()
    {
        var data = GameManager.Instance.Data;

        ResetBaseStat();
        ApplyPermanentUpgrade();
    }

    void ApplyPermanentUpgrade()
    {
        var data = GameManager.Instance.Data;

        attackPower = GoldUpGrade.GetAttackPower(data.attackPowerLevel);
        attackSpeed = GoldUpGrade.GetAttackSpeed(data.attackSpeedLevel);
        moveSpeed = GoldUpGrade.GetMoveSpeed(data.moveSpeedLevel);
        maxHP = GoldUpGrade.GetHP(data.hpLevel);

        OnAttackPowerChanged?.Invoke(attackPower);
        OnMaxHpChanged?.Invoke(maxHP);
        OnAttackSpeedChanged?.Invoke(attackSpeed);
        OnMoveSpeedChanged?.Invoke(moveSpeed);
    }

    void ResetBaseStat()
    {
        attackPower = 10;
        attackSpeed = 1;
        moveSpeed = 5;
        maxHP = 100;
    }

    public void ApplyUpgrade(UpGradeData option)
    {
        if (option.type != UpGradeType.Stat)
            return;

        switch (option.statType)
        {
            case StatType.AttackPower:
                attackPower += option.value;
                OnAttackPowerChanged?.Invoke(attackPower);
                break;

            case StatType.AttackSpeed:
                attackSpeed += option.value;
                break;

            case StatType.MoveSpeed:
                moveSpeed += option.value;
                break;

            case StatType.MaxHp:
                maxHP += option.value;
                OnMaxHpChanged?.Invoke(maxHP);
                break;

        }
    }
}
