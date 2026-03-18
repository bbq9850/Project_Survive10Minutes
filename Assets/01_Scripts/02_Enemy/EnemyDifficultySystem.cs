using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDifficultySystem : MonoBehaviour
{
    
    public static EnemyDifficultySystem Instance;

    float runningTime;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        runningTime += Time.deltaTime;
    }

    public float GetTime()
    {
        return runningTime;
    }
    public float GetMinute()
    {
        return runningTime / 60f;
    }

    public float GetHpMultiplier()
    {
        float minute = GetMinute();

        return Mathf.Pow(1.18f, minute) * 0.7f;
    }

    public float GetDamageMultiplier()
    {
        float minute = GetMinute();

        return Mathf.Pow(1.10f, minute);
    }

    public float GetSpawnMultiplier()
    {
        float minute = GetMinute();

        return 1f + (minute * 0.35f);
    }
}
