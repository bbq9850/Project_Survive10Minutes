using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] Text timeText;

    private float runningTime;
    private bool isRunning = true;
    
    void Update()
    {
        if (!isRunning) return;

        runningTime += Time.deltaTime;
        TimeUI();
    }

    void TimeUI()
    {
        int minutes = Mathf.FloorToInt(runningTime / 60f);
        int seconds = Mathf.FloorToInt(runningTime % 60f);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }

    public void PauseTime()
    {
        isRunning = false;
    }

    public void ResumeTime()
    {
        isRunning = true;
    }

    public float GetGameTimer()
    {
        return runningTime;
    }
}
