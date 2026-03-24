using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    
    [SerializeField] Text valueText;
    [SerializeField] Button button;
    [SerializeField] Image cardBackground;

    [Header("Stat BG")]
    [SerializeField] Sprite attackPowerBG;
    [SerializeField] Sprite attackSpeedBG;
    [SerializeField] Sprite moveSpeedBG;
    [SerializeField] Sprite maxHpBG;

    [Header("Weapon BG")]
    [SerializeField] Sprite MagicArrowBG;
    [SerializeField] Sprite explosionBG;
    UpGradeData option;

    public void Setup(UpGradeData data)
    {
        option = data;

        valueText.text = GetValueText(data);

        SetCardVisual(data);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        LevelUpManager.Instance.SelectOption(option);
    }

    string GetValueText(UpGradeData d)
    {
        switch (d.type)
        {
            case UpGradeType.Stat:
                return GetStatText(d);

            case UpGradeType.WeaponUnlock:
                return $"GET {d.weaponData.weaponType}";

            case UpGradeType.WeaponUpgrade:
                return $"UPGRADE {d.weaponData.weaponType}";
        }

        return "";
    }

    string GetStatText(UpGradeData d)
    {
        switch (d.statType)
        {
            case StatType.AttackPower:
                return $"ATTACK POWER +{d.value}";

            case StatType.AttackSpeed:
                return $"ATTACK SPEED +{d.value * 100f}%";

            case StatType.MoveSpeed:
                return $"MOVE SPEED +{d.value * 100f}%";

            case StatType.MaxHp:
                return $"MAX HP +{d.value}";
        }

        return "";
    }

    void SetCardVisual(UpGradeData data)
    {
        if (data.type == UpGradeType.Stat)
        {
            switch (data.statType)
            {
                case StatType.AttackPower:
                    cardBackground.sprite = attackPowerBG;
                    break;

                case StatType.AttackSpeed:
                    cardBackground.sprite = attackSpeedBG;
                    break;

                case StatType.MoveSpeed:
                    cardBackground.sprite = moveSpeedBG;
                    break;

                case StatType.MaxHp:
                    cardBackground.sprite = maxHpBG;
                    break;
            }
        }

        if (data.type == UpGradeType.WeaponUnlock)
        {
            switch (data.weaponData.weaponType)
            {
                case WeaponType.MagicArrow:
                    cardBackground.sprite = MagicArrowBG;
                    break;
            }
        }

        if (data.type == UpGradeType.WeaponUpgrade)
        {
            switch (data.weaponData.weaponType)
            {
                case WeaponType.MagicArrow:
                    cardBackground.sprite = MagicArrowBG;
                    break;

                case WeaponType.Explosion:
                    cardBackground.sprite = explosionBG;
                    break;

            }
        }
    }
}
