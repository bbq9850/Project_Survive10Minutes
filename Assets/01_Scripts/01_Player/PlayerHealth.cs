using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float, float> OnHpChanged;
    public event Action OnDead;
    public event Action<float> OnDamaged;
    PlayerStat stat;
    
    private float currentHp;

    bool isInvincible;

    public float CurrentHP => currentHp;
    

    private bool playerIsDead;

    private void Awake()
    {
        stat = GetComponent<PlayerStat>();
        currentHp = stat.maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (GodModManager.Instance != null && GodModManager.Instance.godMode)
            return;

        if (isInvincible) return;

    StartCoroutine(InvincibleTime());

        if (playerIsDead)
        {
            return;
        }
        else
        {
            currentHp -= damage;
            currentHp = Mathf.Clamp(currentHp, 0, stat.maxHP);
            OnDamaged?.Invoke(damage);

            OnHpChanged?.Invoke(currentHp, stat.maxHP);
            Camera_QuarterViewRot.Instance.Shake(1.2f, 1.2f);
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

    IEnumerator InvincibleTime()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;
    }


    void Update()
    {
        
    }
}
