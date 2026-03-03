using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] LevelUpButton buttonPrefab;
    [SerializeField] Transform buttonParent;

    List<LevelUpButton> buttons = new();


    void Start()
    {
        
    }

    public void Open(List<UpGradeData> options)
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);

        foreach (var btn in buttons)
            Destroy(btn.gameObject);

        buttons.Clear();

        foreach (var option in options)
        {
            var btn = Instantiate(buttonPrefab, buttonParent);
            btn.Setup(option);
            buttons.Add(btn);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
