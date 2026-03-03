using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Text valueText;
    [SerializeField] Image icon;
    [SerializeField] Button button;

    UpGradeData option;

    public void Setup(UpGradeData data)
    {
        option = data;

        titleText.text = data.upGradeName;
        icon.sprite = data.icon;
        valueText.text = GetValueText(data);

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
}
