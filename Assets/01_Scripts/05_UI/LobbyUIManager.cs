using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] GameObject sellectStagePanel;
    [SerializeField] GameObject optionPanel;

    UITween sellectTween;
    UITween optionTween;

    void Awake()
    {
        sellectTween = sellectStagePanel.GetComponent<UITween>();
        optionTween = optionPanel.GetComponent<UITween>();
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
}
