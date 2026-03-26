using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] GameObject sellectStagePanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject challengePanel;

    UITween sellectTween;
    UITween optionTween;
    UITween challengeTween;

    void Awake()
    {
        sellectTween = sellectStagePanel.GetComponent<UITween>();
        optionTween = optionPanel.GetComponent<UITween>();
        challengeTween = challengePanel.GetComponent<UITween>();
    }

    public void OnSellectStage()
    {
        sellectStagePanel.SetActive(true);
        sellectTween?.PlayOpen();
    }

    public void QuitSellectStage()
    {
        sellectStagePanel.SetActive(false);
        sellectTween?.PlayClose();
    }

    public void OnOption()
    {
        optionPanel.SetActive(true);
        optionTween?.PlayOpen();
    }

    public void QuitOption()
    {
        optionPanel.SetActive(false);
        optionTween?.PlayClose();
    }

    public void OnChallenge()
    {
        challengePanel.SetActive(true);
        challengeTween?.PlayOpen();
    }

    public void QuitChallenge()
    {
        challengePanel.SetActive(false);
        challengeTween?.PlayClose();
    }

    // 관리용 리셋버튼 (추후 삭제)
    public void OnClick_ResetData()
    {
        GameManager.Instance.ResetGameData();
    }
}
