using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    
    [SerializeField] Text valueText;
    [SerializeField] Image icon;
    [SerializeField] Button button;

    [SerializeField] Image cardBackground;

    [SerializeField] Sprite attackPowerBG;
    [SerializeField] Sprite attackSpeedBG;
    [SerializeField] Sprite moveSpeedBG;
    [SerializeField] Sprite maxHpBG;

    UpGradeData option;

    public void Setup(UpGradeData data)
    {
        option = data;

        
        icon.sprite = data.icon;
        valueText.text = GetValueText(data);

        SetCardVisual(data.type);

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
            case UpGradeType.AttackPower:
                return $"공격력 +{d.value}";

            case UpGradeType.AttackSpeed:
                return $"공격속도 증가 +{d.value * 100f}% ";

            case UpGradeType.MoveSpeed:
                return $"이동속도 증가+{d.value * 100f}%";

            case UpGradeType.MaxHp:
                return $"최대 체력+{d.value}";
        }

        return "";
    }

    void SetCardVisual(UpGradeType type)
    {
        switch (type)
        {
            case UpGradeType.AttackPower:
                cardBackground.sprite = attackPowerBG;
                break;

            case UpGradeType.AttackSpeed:
                cardBackground.sprite = attackSpeedBG;
                break;

            case UpGradeType.MoveSpeed:
                cardBackground.sprite = moveSpeedBG;
                break;

            case UpGradeType.MaxHp:
                cardBackground.sprite = maxHpBG;
                break;
        }
    }
}
