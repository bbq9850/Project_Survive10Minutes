using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackWeapon : WeaponBase
{
    protected override void Attack()
    {
        EnemyCore target = FindClosestEnemy();
        if (target == null) return;

        float damage = stat.attackPower * weaponPower;

        target.TakeDamage(damage);

        HitEffectPool.Instance.Play(target.transform.position);
    }

    EnemyCore FindClosestEnemy()
    {
        EnemyCore closest = null;
        float minDist = float.MaxValue;

        foreach (var enemy in EnemyManager.instance.ActiveEnemies)
        {
            if (enemy == null) continue;
            if (!enemy.gameObject.activeInHierarchy) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (dist < minDist && dist <= weaponRange)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }
}
