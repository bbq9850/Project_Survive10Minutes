using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hpSliderText;

    [Header("Stamina")]
    [SerializeField] PlayerStamina playerStamina;
    [SerializeField] Slider staminaSlider;
    [SerializeField] Text staminaText;

    [SerializeField] private PlayerStat stat;

    private void Awake()
    {
        
    }
    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHpChanged += UpdateHpBar;
        }
        if(stat != null)
        {
            stat.OnMaxHpChanged += OnMaxHpChanged;
        }
        if(playerStamina != null)
        {
            playerStamina.OnStaminaChanged += UpdateStaminaBar;
        }

        UpdateHpBar(playerHealth.CurrentHP, stat.maxHP);
        UpdateStaminaBar(playerStamina.CurrentStamina, playerStamina.MaxStamina);
    }

    void OnMaxHpChanged(float newMax)
    {
        UpdateHpBar(playerHealth.CurrentHP, newMax);
    }

    void UpdateHpBar(float current, float max)
    {
        hpSlider.value = current / max;
        hpSliderText.text = $"{current}/{max}";
    }

    void UpdateStaminaBar(float current, float max)
    {
        staminaSlider.value = current / max;
        staminaText.text = $"{current:0}/{max:0}";
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHpChanged -= UpdateHpBar;
        }
            

        if (stat != null)
        {
            stat.OnMaxHpChanged -= OnMaxHpChanged;
        }

        if (playerStamina != null)
        {
            playerStamina.OnStaminaChanged -= UpdateStaminaBar;
        }
    }
}
