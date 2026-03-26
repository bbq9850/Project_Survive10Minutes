using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance;

    [SerializeField] List<ChallengeSO> challenges;
    public List<ChallengeSO> Challenges => challenges;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.OnKillCountChanged += CheckChallenges;
        GameManager.Instance.OnGoldChanged += CheckChallenges;
        CheckChallenges(0);
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnKillCountChanged -= CheckChallenges;
            GameManager.Instance.OnGoldChanged -= CheckChallenges;
        }
    }

    void CheckChallenges(int _)
    {

        var data = GameManager.Instance.Data;

        foreach (var ch in challenges)
        {

            if (data.clearedChallenges.Contains(ch.id))
                continue;

            int current = GetCurrentValue(ch);
            // Debug.Log($"{ch.challengeName} : {current}/{ch.goal}");

            if (current >= ch.goal)
            {
                data.clearedChallenges.Add(ch.id);
                Debug.Log($"CHALLGE CLEAR: {ch.challengeName}");
            }
        }
    }

    int GetCurrentValue(ChallengeSO ch)
    {
        var data = GameManager.Instance.Data;

        switch (ch.type)
        {
            case ChallengeType.Kill:
                return data.killCount;

            case ChallengeType.Gold:
                return data.gold;
        }

        return 0;
    }

    public void ClaimReward(ChallengeSO ch)
    {
        var data = GameManager.Instance.Data;

        if (!data.clearedChallenges.Contains(ch.id))
            return;

        if (data.rewardedChallenges.Contains(ch.id))
            return;

        data.rewardedChallenges.Add(ch.id);

        GameManager.Instance.AddGold(ch.rewardGold);
        GameManager.Instance.SaveGame();

        Debug.Log($"º¸»ó È¹µæ: {ch.rewardGold}G");
    }

    public float GetProgress(ChallengeSO ch)
    {
        int current = GetCurrentValue(ch);
        return Mathf.Clamp01((float)current / ch.goal);
    }

    public int GetCurrent(ChallengeSO ch)
    {
        return GetCurrentValue(ch);
    }
}
