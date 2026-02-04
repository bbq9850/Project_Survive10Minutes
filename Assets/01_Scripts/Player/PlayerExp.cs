using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public int level = 1;
    public int currentExp;
    public int expToNext = 5;

    public void AddExp(int amount)
    {
        currentExp += amount;
        Debug.Log($"EXP : {currentExp} / {expToNext}");
        if (currentExp >= expToNext)
            LevelUp();
    }

    void LevelUp()
    {
        currentExp -= expToNext;
        level++;
        expToNext = Mathf.RoundToInt(expToNext * 1.4f);

        Debug.Log($"LEVEL UP! ¡æ {level}");
    }
}
