using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameData Data {  get; private set; }

    public System.Action<int> OnGoldChanged;
    public System.Action<int> OnKillCountChanged;
    public Action<int> OnStageChanged;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGame();
    }

    public void AddGold(int amount)
    {
        Data.gold += amount;
        OnGoldChanged?.Invoke(Data.gold);

        SaveGame();
    }

    public void AddKill()
    {
        Data.killCount ++;
        OnKillCountChanged?.Invoke(Data.killCount);
    }
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString("SAVE_DATA", json);
        PlayerPrefs.Save();

        Debug.Log("저장 완료");
    }
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SAVE_DATA"))
        {
            string json = PlayerPrefs.GetString("SAVE_DATA");
            Data = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Data = new GameData(); 
        }

        Debug.Log("로드 완료");
    }

    public void ResetGameData()
    {
        PlayerPrefs.DeleteKey("SAVE_DATA");
        PlayerPrefs.Save();

        Data = new GameData();

        OnGoldChanged?.Invoke(Data.gold);
        OnKillCountChanged?.Invoke(Data.killCount);

        Debug.Log("데이터 초기화 완료");
    }

    public bool TryUpgradeAttack()
    {
        int level = Data.attackPowerLevel;

        if (level >= GoldUpGrade.MAX_LEVEL)
            return false;

        int cost = GoldUpGrade.GetUpgradeCost(level);

        if (Data.gold < cost) return false;

        Data.gold -= cost;
        Data.attackPowerLevel++;

        OnGoldChanged?.Invoke(Data.gold);
        SaveGame();

        return true;
    }

    public bool TryUpgradeHP()
    {
        int level = Data.hpLevel;

        if (level >= GoldUpGrade.MAX_LEVEL)
            return false;

        int cost = GoldUpGrade.GetUpgradeCost(level);

        if (Data.gold < cost) return false;

        Data.gold -= cost;
        Data.hpLevel++;

        OnGoldChanged?.Invoke(Data.gold);
        SaveGame();

        return true;
    }

    public bool TryUpgradeAS()
    {
        int level = Data.attackSpeedLevel;

        if (level >= GoldUpGrade.MAX_LEVEL)
            return false;

        int cost = (int)GoldUpGrade.GetAttackSpeed(level);

        if(Data.gold < cost) return false;

        Data.gold -= cost;
        Data.attackSpeedLevel++;

        OnGoldChanged?.Invoke(Data.gold);
        SaveGame();

        return true;
    }

    public bool TryUpgradeMoveSpeed()
    {
        int level = Data.moveSpeedLevel;

        if (level >= GoldUpGrade.MAX_LEVEL)
            return false;

        int cost = GoldUpGrade.GetUpgradeCost(level);

        if (Data.gold < cost) return false;

        Data.gold -= cost;
        Data.moveSpeedLevel++;

        OnGoldChanged?.Invoke(Data.gold);
        SaveGame();

        return true;
    }
}
