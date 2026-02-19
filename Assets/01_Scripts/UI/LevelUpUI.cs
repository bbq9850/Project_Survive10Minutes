using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] GameObject upGradePanel;
    [SerializeField] Text UpGradeText;


    void Start()
    {
        upGradePanel.SetActive(false);
    }

    public void Open(int level)
    {
        Time.timeScale = 0f;
        upGradePanel.SetActive(true);
        UpGradeText.text = $"레벨 업!! 현재 레벨:{level}";
    }

    public void Close()
    {
        upGradePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
