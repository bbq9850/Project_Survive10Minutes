using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    [SerializeField] StageClearUI stageClearUI;
    UITween tween;

    bool isStageClear;

    void Awake()
    {
        Instance = this;
        tween = stageClearUI.GetComponent<UITween>();
    }

    public void OnBossDead()
    {
        if (isStageClear) return;

        isStageClear = true;

        Debug.Log("Stage Clear!");

        StageClearUI.Instance.Show();
        tween.PlayOpen();

        StopGameTime();
    }

    void StopGameTime()
    {
        Time.timeScale = 0f;
    }
}
