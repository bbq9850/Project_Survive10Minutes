using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float, float> OnHpChanged;
    public event Action OnDead;
    PlayerStat stat;
    
    private float currentHp;

    public float CurrentHP => currentHp;
    

    private bool playerIsDead;

    private void Awake()
    {
        stat = GetComponent<PlayerStat>();
        currentHp = stat.maxHP;
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
            currentHp = Mathf.Clamp(currentHp, 0, stat.maxHP);
            Debug.Log($"HP : {currentHp} / {stat.maxHP}");

            OnHpChanged?.Invoke(currentHp, stat.maxHP);
        }

        if(currentHp <= 0)
        {
            Die();
        }

    }

    public void PlayerHeal(float heal)
    {
        currentHp += heal;
        currentHp = Mathf.Clamp(currentHp, 0, stat.maxHP);

        OnHpChanged?.Invoke(currentHp, stat.maxHP);
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
