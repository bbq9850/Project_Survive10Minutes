using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    [SerializeField] Text goldCountText;


    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("GameManager Null!");
            return;
        }
        GameManager.Instance.OnGoldChanged += UpdateGoldCount;
        UpdateGoldCount(GameManager.Instance.Data.gold);
    }

    void UpdateGoldCount(int killCount)
    {
        goldCountText.text = killCount.ToString("D2");
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGoldChanged -= UpdateGoldCount;
    }
}
