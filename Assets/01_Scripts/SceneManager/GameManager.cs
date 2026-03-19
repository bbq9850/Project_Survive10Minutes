using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int gold;
    public System.Action OnGoldChanged;
    public int killCount;
    public System.Action OnKillCountChanged;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke();
    }

    public void AddKillCount(int amount)
    {
        killCount += amount;
        OnKillCountChanged?.Invoke();
    }
}
