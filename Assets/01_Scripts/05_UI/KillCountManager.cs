using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCountManager : MonoBehaviour
{
    public int KillCount {  get; private set; }

    public static KillCountManager Instance { get; private set; }

    public event Action<int> OnKill;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddKill()
    {
        KillCount++;
        OnKill?.Invoke(KillCount);
    }
}
