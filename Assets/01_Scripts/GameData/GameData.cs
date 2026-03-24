using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int gold;
    public int killCount;
    public int stage;
    public float playTime;

    public int attackPowerLevel;
    public int attackSpeedLevel;
    public int moveSpeedLevel;
    public int hpLevel;

    public List<string> clearedChallenges = new List<string>();

    public List<string> rewardedChallenges = new List<string>();
}
