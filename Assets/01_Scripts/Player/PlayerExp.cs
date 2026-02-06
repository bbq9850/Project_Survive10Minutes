using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public int level = 1;
    public int currentExp;
    public int expToNext = 5;

    [SerializeField] LevelUpEffect levelUpEffect;

    public event Action<float, float> OnExpChanged;
    public event Action<int> OnLevelChanged;
    

    public void AddExp(int amount)
    {
        currentExp += amount;
        //Debug.Log($"EXP : {currentExp} / {expToNext}");
        OnExpChanged?.Invoke(currentExp, expToNext);


        if (currentExp >= expToNext)
            LevelUp();
    }

    void LevelUp()
    {
        currentExp -= expToNext;
        level++;
        expToNext = Mathf.RoundToInt(expToNext * 1.4f);
        levelUpEffect.Play();

        OnLevelChanged?.Invoke(level);
        Debug.Log($"LEVEL UP! ¡æ {level}");
    }


}
