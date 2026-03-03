using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{

    public static LevelUpManager Instance;

    [SerializeField] PlayerExp playerExp;

    [SerializeField] List<UpGradeData> upgrades;
    [SerializeField] LevelUpUI levelUpUI;
    [SerializeField] PlayerStat playerStat;

    void Start()
    {
        //playerExp.OnLevelUp += levelUpUI.Open;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void OpenLevelUp()
    {
        List<UpGradeData> selected =
            GetRandomUpGrade(3);

        levelUpUI.Open(selected);

    }

    List<UpGradeData> GetRandomUpGrade(int count)
    {
        List<UpGradeData> result = new();
        List<UpGradeData> pool = new(upgrades);

        for (int i = 0; i < count; i++)
        {
            if (pool.Count == 0) break;

            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }
        return result;
    }

    public void SelectOption(UpGradeData option)
    {
        playerStat.ApplyUpgrade(option);

        levelUpUI.Close();
    }
}
