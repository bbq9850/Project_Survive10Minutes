using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Challenge")]
public class ChallengeSO : ScriptableObject
{
    public string id;
    public string challengeName;

    public int goal;
    public int rewardGold;

    public ChallengeType type;
}

public enum ChallengeType
{
    Kill,
    Gold
}

