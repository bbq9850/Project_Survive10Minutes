using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] float startTime = 600f;

    private float remainingTime;
    private bool isRunning = true;

    public static GameTimer Instance {get; private set;}
    public float RunningTime { get; set; }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        remainingTime = startTime;
        UpdateTimeUI();
    }

    void Update()
    {
        if (!isRunning) return;
        if (remainingTime <= 0f) return;

        remainingTime -= Time.deltaTime;
        RunningTime += Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isRunning = false;
            OnTimeOver();
        }

        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }

    public float GetMinutes()
    {
        return RunningTime / 60f;
    }

    void OnTimeOver()
    {
        Debug.Log("보스소환");
    }

    public void PauseTime()
    {
        isRunning = false;
    }

    public void ResumeTime()
    {
        isRunning = true;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }
}
