using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hpSliderText;


    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHpChanged += UpdateHpBar;

            UpdateHpBar(playerHealth.CurrentHP, playerHealth.MaxHp);
        }
    }

    void UpdateHpBar(float current, float max)
    {
        hpSlider.value = current / max;
        hpSliderText.text = $"{current}/{max}";
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHpChanged -= UpdateHpBar;
    }
}
