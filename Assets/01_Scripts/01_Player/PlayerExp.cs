using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public static PlayerExp Instance;
    public int Level { get; set; } = 1;
    public int CurrentExp { get; set; }
    public int ExpToNext { get; set; }

    [SerializeField] LevelUpEffect levelUpEffect;

    public event Action<float, float> OnExpChanged;
    public event Action<int> OnLevelUp;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        ExpToNext = GetExpToNext(Level);
        OnExpChanged?.Invoke(CurrentExp, ExpToNext);
    }

    int GetExpToNext(int level)
    {
        return Mathf.RoundToInt(
            8 + level * 7 + Mathf.Pow(level, 1.5f) * 2f
        );
    }

    public void AddExp(int amount)
    {
        CurrentExp += amount;
        while (CurrentExp >= ExpToNext)
        {
            CurrentExp -= ExpToNext;
            LevelUpInternal();
        }

        OnExpChanged?.Invoke(CurrentExp, ExpToNext);
    }

    void LevelUpInternal()
    {
        Level++;

        levelUpEffect?.Play();

        ExpToNext = GetExpToNext(Level);

        OnLevelUp?.Invoke(Level);

        LevelUpManager.Instance.OpenLevelUp();

        Debug.Log($"LEVEL UP! → {Level}");
    }

    //public void AddExp(int amount)
    //{
    //    CurrentExp += amount;
    //    //Debug.Log($"EXP : {currentExp} / {expToNext}");



    //    if (CurrentExp >= ExpToNext)
    //    {
    //        CurrentExp -= ExpToNext;
    //        LevelUp();
    //    }
    //    OnExpChanged?.Invoke(CurrentExp, ExpToNext);
    //}

    //void LevelUp()
    //{

    //    Level++;
    //    ExpToNext = Mathf.RoundToInt(ExpToNext * 1.4f);
    //    levelUpEffect.Play();

    //    OnLevelUp?.Invoke(Level);
    //    Debug.Log($"LEVEL UP! → {Level}");
    //}


}
