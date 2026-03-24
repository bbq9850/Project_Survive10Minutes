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

    [SerializeField] GameObject subPanel;
    [SerializeField] Text subText;

    UITween subTween;

    private void Awake()
    {
        subTween = subPanel.GetComponent<UITween>();    
    }

    void Start()
    {
        RefreshAll();
        GameManager.Instance.OnGoldChanged += RefreshGold;
        subPanel.SetActive(false);
    }

    void RefreshGold(int _ = 0)
    {
        if (goldText == null) return;
        goldText.text = $"{GameManager.Instance.Data.gold}";
        UpdateButtonState();
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGoldChanged -= RefreshGold;
    }

    public void OnClick_AttackPower()
    {
        var data = GameManager.Instance.Data;

        if (data.attackPowerLevel >= GoldUpGrade.MAX_LEVEL)
        {
            MaxLevel();
            return;
        }

        int cost = GoldUpGrade.GetUpgradeCost(data.attackPowerLevel);

        if (data.gold < cost)
        {
            MoreGold();
            return;
        }

        if (GameManager.Instance.TryUpgradeAttack())
        {
            UpgradeComplete();
            RefreshAll();
        }        
    }

    public void OnClick_AttackSpeed()
    {
        var data = GameManager.Instance.Data;

        if (data.attackSpeedLevel >= GoldUpGrade.MAX_LEVEL)
        {
            MaxLevel();
            return;
        }

        int cost = GoldUpGrade.GetUpgradeCost(data.attackSpeedLevel);

        if (data.gold < cost)
        {
            MoreGold();
            return;
        }

        if (GameManager.Instance.TryUpgradeAS())
        {
            UpgradeComplete();
            RefreshAll();
        }
    }

    public void OnClick_MoveSpeed()
    {
        var data = GameManager.Instance.Data;

        if (data.moveSpeedLevel >= GoldUpGrade.MAX_LEVEL)
        {
            MaxLevel();
            return;
        }

        int cost = GoldUpGrade.GetUpgradeCost(data.moveSpeedLevel);

        if (data.gold < cost)
        {
            MoreGold();
            return;
        }

        if (GameManager.Instance.TryUpgradeMoveSpeed())
        {
            UpgradeComplete();
            RefreshAll();
        }
    }

    public void OnClick_HP()
    {
        var data = GameManager.Instance.Data;

        if (data.hpLevel >= GoldUpGrade.MAX_LEVEL)
        {
            MaxLevel();
            return;
        }

        int cost = GoldUpGrade.GetUpgradeCost(data.hpLevel);

        if (data.gold < cost)
        {
            MoreGold();
            return;
        }

        if (GameManager.Instance.TryUpgradeHP())
        {
            UpgradeComplete();
            RefreshAll();
        }
    }

    void RefreshAll()
    {
        var data = GameManager.Instance.Data;

        SetUpgradeText(attackPowerText, "ATK", data.attackPowerLevel, data.gold);
        SetUpgradeText(attackSpeedText, "ATK SPD", data.attackSpeedLevel, data.gold);
        SetUpgradeText(moveSpeedText, "MOVE SPD", data.moveSpeedLevel, data.gold);
        SetUpgradeText(hpText, "MAX HP", data.hpLevel, data.gold);

        attackPowerButton.gameObject.SetActive(data.attackPowerLevel <= GoldUpGrade.MAX_LEVEL);
        attackSpeedButton.gameObject.SetActive(data.attackSpeedLevel <= GoldUpGrade.MAX_LEVEL);
        moveSpeedButton.gameObject.SetActive(data.moveSpeedLevel <= GoldUpGrade.MAX_LEVEL);
        hpButton.gameObject.SetActive(data.hpLevel <= GoldUpGrade.MAX_LEVEL);

        RefreshGold();
    }

    void UpdateButtonState()
    {
        attackPowerButton.interactable = true;
        attackSpeedButton.interactable = true;
        moveSpeedButton.interactable = true;
        hpButton.interactable = true;
    }

    void SetUpgradeText(Text textUI, string label, int level, int gold)
    {
        if (level >= GoldUpGrade.MAX_LEVEL)
        {
            textUI.text = $"{label} Lv.MAX";
            textUI.color = Color.red;
            return;
        }

        int cost = GoldUpGrade.GetUpgradeCost(level);

        textUI.text = $"{label}\n Lv.{level}\nCost : {cost}";
    }

    public void MaxLevel()
    {
        subText.text = "MAX LEVEL";
        subPanel.SetActive(true);
        subTween?.PlayOpen();
    }

    public void MoreGold()
    {
        subText.text = "NEED MORE GOLD";
        subPanel.SetActive(true);
        subTween?.PlayOpen();
    }

    public void UpgradeComplete()
    {
        subText.text = "UPGRADE COMPLETE";
        subPanel.SetActive(true);
        subTween?.PlayOpen();
    }

    public void QuitSubPanel()
    {
        subTween?.PlayClose();
        subPanel.SetActive(false);
    }
}
