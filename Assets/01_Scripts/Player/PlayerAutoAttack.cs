using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoAttack : MonoBehaviour
{
    PlayerStat stat;
    [SerializeField] float attackRange = 5f;
    

    [SerializeField] HitEffect hitEffectPrefab;

    float attackCoolTime;

    void Awake()
    {
        stat = GetComponent<PlayerStat>();
       
    }

    
    void Update()
    {
        attackCoolTime += Time.deltaTime;

        float interval = 1f / stat.attackSpeed;

        if (attackCoolTime >= interval)
        {
            attackCoolTime = 0f;
            AutoAttack();
        }
    }

    void AutoAttack()
    {
        EnemyCore target = FindClosestEnemy();
        if (target == null) return;

        target.TakeDamage(stat.attackPower);
        HitEffectPool.Instance.Play(target.transform.position);
    }

    

    EnemyCore FindClosestEnemy()
    {
        EnemyCore closest = null;
        float minDist = float.MaxValue;

        foreach (var enemy in EnemyManager.instance.ActiveEnemies)
        {
            if(enemy == null) continue;
            if (!enemy.gameObject.activeInHierarchy) continue;
            if(enemy.data == null) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            if (dist < minDist && dist <= attackRange)
            {
                minDist = dist;
                closest = enemy;
            }
        }
        return closest;
    }
}
