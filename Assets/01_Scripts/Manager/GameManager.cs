using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameData Data {  get; private set; }

    public int gold;
    public int killCount;

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

    private void Update()
    {
        
    }

    public void AddGold(int amount)
    {
        Data.gold += amount;
        OnGoldChanged?.Invoke(Data.gold);
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
}
