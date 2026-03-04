using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float maxHP;
    [SerializeField]private float currentHP;
    private EnemyCore enemyCore;

    bool isDead;

    private void Awake()
    {
        enemyCore = GetComponent<EnemyCore>();
    }

    public void Init(float maxHP)
    {
        this.maxHP = maxHP;
        currentHP = maxHP;
        isDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        currentHP -= damage;

        if(currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        enemyCore.OnDeadEnemy();

    }

}
