using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar_LvText : MonoBehaviour
{
    [SerializeField] Slider expSlider;
    [SerializeField] Text lvText;

    [SerializeField] PlayerExp playerExp;

    void Start()
    {
        playerExp.OnExpChanged += UpdateExpBar;
        playerExp.OnLevelChanged += UpdateLevelText;
    }

    void UpdateExpBar(float cur, float max)
    {
        expSlider.value = cur / max;
    }

    void UpdateLevelText(int level)
    {
        lvText.text = $"Lv {level:00}";
    }
}
