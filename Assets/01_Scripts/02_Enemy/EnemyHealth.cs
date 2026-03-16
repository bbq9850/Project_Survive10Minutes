using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float maxHP;
    [SerializeField]private float currentHP;
    private EnemyCore enemyCore;
    EnemyData enemyData;

    bool isDead;

    private void Awake()
    {
        enemyCore = GetComponent<EnemyCore>();
    }

    public void Init(float maxHP, EnemyData data)
    {
        this.maxHP = maxHP;
        currentHP = maxHP;
        isDead = false;

        enemyData = data;
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

        if (enemyData != null && enemyData.isBoss)
        {
            StageManager.Instance?.OnBossDead();
        }

        isDead = true;

        enemyCore.OnDeadEnemy();

    }

}
