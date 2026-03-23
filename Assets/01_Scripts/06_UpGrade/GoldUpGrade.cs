using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldUpGrade : MonoBehaviour
{
    public static int MAX_LEVEL = 10;

    public static float GetAttackPower(int level)
    {
        return 10 + level * 2;
    }

    public static float GetAttackSpeed(int level)
    {
        return 1 + level * 0.1f;
    }

    public static float GetHP(int level)
    {
        return 100 + level * 20;
    }

    public static float GetMoveSpeed(int level)
    {
        return 5 + level * 0.3f;
    }

    public static int GetUpgradeCost(int level)
    {
        return 100 + level * level * 20;
    }
}
