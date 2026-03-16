using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    [SerializeField] StageClearUI stageClearUI;

    bool isStageClear;

    void Awake()
    {
        Instance = this;
    }

    public void OnBossDead()
    {
        if (isStageClear) return;

        isStageClear = true;

        Debug.Log("Stage Clear!");

        Time.timeScale = 0f;

        StageClearUI.Instance.Show();
    }
}
