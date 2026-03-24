using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeItemUI : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text progressText;
    [SerializeField] Slider progressBar;
    [SerializeField] Button claimButton;
    [SerializeField] Text buttonText;

    ChallengeSO ch;

    public void Init(ChallengeSO challenge)
    {
        ch = challenge;

        claimButton.onClick.AddListener(OnClickClaim);

        Refresh();
    }

    public void Refresh()
    {
        var manager = ChallengeManager.Instance;
        var data = GameManager.Instance.Data;

        nameText.text = ch.challengeName;

        int current = manager.GetCurrent(ch);
        progressText.text = $"{current} / {ch.goal}";

        progressBar.value = manager.GetProgress(ch);

        bool cleared = data.clearedChallenges.Contains(ch.id);
        bool rewarded = data.rewardedChallenges.Contains(ch.id);

        if (!cleared)
        {
            buttonText.text = "IN PROGRESS";
            claimButton.interactable = false;
        }
        else if (rewarded)
        {
            buttonText.text = "CLEAR";
            claimButton.interactable = false;
        }
        else
        {
            buttonText.text = "GET REWARD";
            claimButton.interactable = true;
        }
    }

    void OnClickClaim()
    {
        ChallengeManager.Instance.ClaimReward(ch);
        Refresh();
    }
}
