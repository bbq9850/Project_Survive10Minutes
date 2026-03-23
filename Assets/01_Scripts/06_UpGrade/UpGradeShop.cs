using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeShop : MonoBehaviour
{

    [SerializeField] Text goldText;

    [SerializeField] Text attackPowerText;
    [SerializeField] Text attackSpeedText;
    [SerializeField] Text moveSpeedText;
    [SerializeField] Text hpText;

    [SerializeField] Button attackPowerButton;
    [SerializeField] Button attackSpeedButton;
    [SerializeField] Button moveSpeedButton;
    [SerializeField] Button hpButton;

    [SerializeField] Color normarColor = Color.white;
    [SerializeField] Color lackColor = Color.red;
    [SerializeField] Color maxColor = Color.yellow;

    void Start()
    {
        RefreshAll();
        GameManager.Instance.OnGoldChanged += RefreshGold;
    }

    void RefreshGold(int _ = 0)
    {
        goldText.text = $"{GameManager.Instance.Data.gold}";
        UpdateButtonState();
    }

    public void OnClick_AttackPower()
    {
        if (GameManager.Instance.TryUpgradeAttack())
            RefreshAll();
    }

    public void OnClick_AttackSpeed()
    {
        if (GameManager.Instance.TryUpgradeAS())
            RefreshAll();
    }

    public void OnClick_MoveSpeed()
    {
        if (GameManager.Instance.TryUpgradeMoveSpeed())
            RefreshAll();
    }

    public void OnClick_HP()
    {
        if (GameManager.Instance.TryUpgradeHP())
            RefreshAll();
    }

    void RefreshAll()
    {
        var data = GameManager.Instance.Data;

        SetUpgradeText(attackPowerText, "ATK", data.attackPowerLevel, data.gold);
        SetUpgradeText(attackSpeedText, "ATK SPD", data.attackSpeedLevel, data.gold);
        SetUpgradeText(moveSpeedText, "MOVE SPD", data.moveSpeedLevel, data.gold);
        SetUpgradeText(hpText, "MAX HP", data.hpLevel, data.gold);

        attackPowerButton.gameObject.SetActive(data.attackPowerLevel < GoldUpGrade.MAX_LEVEL);
        attackSpeedButton.gameObject.SetActive(data.attackSpeedLevel < GoldUpGrade.MAX_LEVEL);
        moveSpeedButton.gameObject.SetActive(data.moveSpeedLevel < GoldUpGrade.MAX_LEVEL);
        hpButton.gameObject.SetActive(data.hpLevel < GoldUpGrade.MAX_LEVEL);

        RefreshGold();
    }

    void UpdateButtonState()
    {
        var data = GameManager.Instance.Data;

        attackPowerButton.interactable =
        data.attackPowerLevel < GoldUpGrade.MAX_LEVEL &&
        data.gold >= GoldUpGrade.GetUpgradeCost(data.attackPowerLevel);

        attackSpeedButton.interactable =
            data.attackSpeedLevel < GoldUpGrade.MAX_LEVEL &&
            data.gold >= GoldUpGrade.GetUpgradeCost(data.attackSpeedLevel);

        moveSpeedButton.interactable =
            data.moveSpeedLevel < GoldUpGrade.MAX_LEVEL &&
            data.gold >= GoldUpGrade.GetUpgradeCost(data.moveSpeedLevel);

        hpButton.interactable =
            data.hpLevel < GoldUpGrade.MAX_LEVEL &&
            data.gold >= GoldUpGrade.GetUpgradeCost(data.hpLevel);
    }

    void SetUpgradeText(Text textUI, string label, int level, int gold)
    {
        if (level >= GoldUpGrade.MAX_LEVEL)
        {
            textUI.text = $"{label} Lv.MAX";
            textUI.color = Color.yellow;
            return;
        }

        int cost = GoldUpGrade.GetUpgradeCost(level);

        textUI.text = $"{label}\n Lv.{level}\nCost : {cost}";

        if (gold < cost)
            textUI.color = Color.red;
        else
            textUI.color = Color.white;
    }
}
