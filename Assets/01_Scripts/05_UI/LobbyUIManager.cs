using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] GameObject sellectStagePanel;

    UITween tween;

    void Awake()
    {
        tween = sellectStagePanel.GetComponent<UITween>();
    }

    public void OnSellectStage()
    {
        sellectStagePanel.SetActive(true);
        tween.PlayOpen();
    }

    public void QuitSellectStage()
    {
        sellectStagePanel.SetActive(false);
    }
}
