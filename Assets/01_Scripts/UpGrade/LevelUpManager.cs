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

    private void Awake()
    {
        Instance = this;
    }

    public void OpenLevelUp()
    {
        List<UpGradeData> selected = GetRandomUpgrades(3);

        levelUpUI.Open(selected);
    }

    List<UpGradeData> GetRandomUpgrades(int count)
    {
        List<UpGradeData> pool = GetUpgradePool();
        List<UpGradeData> result = new();

        for (int i = 0; i < count; i++)
        {
            if (pool.Count == 0)
                break;

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

                //WeaponBase newWeapon =
                //weaponController.GetWeapon(option.weaponData);

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
