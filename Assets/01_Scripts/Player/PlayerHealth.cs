using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float, float> OnHpChanged;
    public event Action OnDead;

    [SerializeField] private float maxHp = 100f;
    private float currentHp;

    public float CurrentHP => currentHp;
    public float MaxHp => maxHp;

    private bool playerIsDead;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (playerIsDead)
        {
            return;
        }
        else
        {
            currentHp -= damage;
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);
            Debug.Log($"HP : {currentHp} / {maxHp}");

            OnHpChanged?.Invoke(currentHp, maxHp);
        }

        if(currentHp <= 0)
        {
            Die();
        }

    }

    public void PlayerHeal(float heal)
    {
        currentHp += heal;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        OnHpChanged?.Invoke(currentHp, maxHp);
    }

    private void Die()
    {
        playerIsDead = true;
        OnDead?.Invoke();
        Debug.Log("PlayerDead");
    }

    
    void Update()
    {
        
    }
}
