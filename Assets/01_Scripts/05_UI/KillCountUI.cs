using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] Text killCountText;


    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("GameManager Null!");
            return;
        }
        GameManager.Instance.OnKillCountChanged += UpdateKillCount;
        UpdateKillCount(GameManager.Instance.Data.killCount);
    }

    void UpdateKillCount(int killCount)
    {
        killCountText.text = killCount.ToString("D2");
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnKillCountChanged -= UpdateKillCount;
    }
}
