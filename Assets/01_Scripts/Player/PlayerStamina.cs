using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] float maxStamina = 50f;
    [SerializeField] float regenRate = 5f;

    public float CurrentStamina { get; private set; }
    public float MaxStamina => maxStamina;

    public event Action<float, float> OnStaminaChanged;

    void Awake()
    {
        CurrentStamina = maxStamina;
        OnStaminaChanged?.Invoke(CurrentStamina, maxStamina);
    }

    void Update()
    {
        Regenerate();
    }

    void Regenerate()
    {
        if (CurrentStamina >= maxStamina) return;

        CurrentStamina += regenRate * Time.deltaTime;
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, maxStamina);

        OnStaminaChanged?.Invoke(CurrentStamina, maxStamina);
    }
    public bool TryUse(float amount)
    {
        if (CurrentStamina < amount)
            return false;

        CurrentStamina -= amount;
        OnStaminaChanged?.Invoke(CurrentStamina, maxStamina);
        return true;
    }
}
