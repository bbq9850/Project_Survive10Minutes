using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] Text killCountText;


    void Start()
    {
        KillCountManager.Instance.OnKill += UpdateKillCount;
        UpdateKillCount(KillCountManager.Instance.KillCount);
    }

    void UpdateKillCount(int killCount)
    {
        killCountText.text = $"{killCount:00}";
    }

    private void OnDestroy()
    {
        if (KillCountManager.Instance != null)
            KillCountManager.Instance.OnKill -= UpdateKillCount;
    }
}
