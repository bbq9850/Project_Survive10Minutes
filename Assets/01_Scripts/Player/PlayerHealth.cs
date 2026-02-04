using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float> OnDamaged;
    public event Action OnDead;

    [SerializeField] private float maxHP = 100f;
    public float CurrentHP {  get; private set; }

    private bool playerIsDead;

    private void Awake()
    {
        CurrentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (playerIsDead)
        {
            return;
        }
        else
        {
            CurrentHP -= damage;
            CurrentHP = Mathf.Max(CurrentHP, 0);
            Debug.Log($"HP : {CurrentHP} / {maxHP}");

            OnDamaged?.Invoke(damage);
        }

        if(CurrentHP <= 0)
        {
            Die();
        }

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
