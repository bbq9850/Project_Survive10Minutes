using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{

    public static LevelUpManager Instance;

    [SerializeField] PlayerExp playerExp;

    [SerializeField] PlayerHUD playerHUD;

    [SerializeField] List<UpGradeData> upgrades;

    [SerializeField] LevelUpUI levelUpUI;
    [SerializeField] PlayerStat playerStat;
    [SerializeField] PlayerWeaponController weaponController;

    UITween tween;

    private void Awake()
    {
        Instance = this;
        tween = levelUpUI.GetComponent<UITween>();
    }

    public void OpenLevelUp()
    {
        List<UpGradeData> selected = GetRandomUpgrades(3);

        levelUpUI.Open(selected);
        tween.PlayOpen();
    }

    List<UpGradeData> GetRandomUpgrades(int count)
    {
        List<UpGradeData> pool = GetUpgradePool();

        List<UpGradeData> weaponPool = new();
        List<UpGradeData> statPool = new();

        foreach (var upgrade in pool)
        {
            if (upgrade.type == UpGradeType.Stat)
                statPool.Add(upgrade);
            else
                weaponPool.Add(upgrade);
        }

        List<UpGradeData> result = new();

        if (weaponPool.Count > 0)
        {
            int index = Random.Range(0, weaponPool.Count);
            result.Add(weaponPool[index]);
            pool.Remove(weaponPool[index]);
        }

        while (result.Count < count && pool.Count > 0)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }

        return result;
    }

    List<UpGradeData> GetUpgradePool()
    {
        List<UpGradeData> pool = new();

        foreach (var upgrade in upgrades)
        {
            if (IsUpgradeAvailable(upgrade))
                pool.Add(upgrade);
        }

        return pool;
    }
    bool IsUpgradeAvailable(UpGradeData upgrade)
    {
        
        switch (upgrade.type)
        {
            case UpGradeType.Stat:
                return true;

            case UpGradeType.WeaponUnlock:

                if (upgrade.weaponData.weaponType == WeaponType.Explosion)
                    return false;

                if (weaponController.HasWeapon(upgrade.weaponData))
                    return false;

                return true;

            case UpGradeType.WeaponUpgrade:

                if (!weaponController.HasWeapon(upgrade.weaponData))
                    return false;

                WeaponBase weapon =
                    weaponController.GetWeapon(upgrade.weaponData);

                if (weapon != null && weapon.IsMaxLevel())
                    return false;

                return true;
        }

        return false;
    }
    public void SelectOption(UpGradeData option)
    {
        switch (option.type)
        {
            case UpGradeType.WeaponUnlock:

                weaponController.AddWeapon(option.weaponData);
                break;

            case UpGradeType.WeaponUpgrade:

                WeaponBase weapon =
                    weaponController.GetWeapon(option.weaponData);

                if (weapon != null)
                    weapon.LevelUp(1);

                playerHUD.RefreshWeaponUI();

                break;

            case UpGradeType.Stat:

                playerStat.ApplyUpgrade(option);

                playerHUD.RefreshStatUI();
                break;
        }

        levelUpUI.Close();
    }
}
