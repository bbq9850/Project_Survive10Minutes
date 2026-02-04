using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float maxHP;
    private float currentHP;
    private EnemyCore enemyCore;

    private void Awake()
    {
        enemyCore = GetComponent<EnemyCore>();
    }

    public void Init(float maxHP)
    {
        this.maxHP = maxHP;
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        //Debug.Log($"{gameObject.name} currentHP : {currentHP}");
        
        if(currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ExpOrbPool.Instance.Spawn(
            transform.position, enemyCore.data.expValue);
        enemyCore.OnDeadEnemy();
    }

}
