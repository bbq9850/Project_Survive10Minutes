using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Kunai : WeaponBase
{
    [SerializeField] GameObject projectilePrefab;

    protected override void Attack()
    {
        EnemyCore target = FindClosestEnemy();
        if (target == null) return;

        Vector3 dir =
            (target.transform.position - transform.position).normalized;
        dir.y = 0f;

        GameObject arrow =
            ProjectilePool.Instance.Get();

        arrow.transform.position = transform.position;

        arrow.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(90, 0, 0);

        arrow.GetComponent<Projectile>()
            .Init(dir, stat.attackPower);
    }

    EnemyCore FindClosestEnemy()
    {
        EnemyCore closest = null;
        float minDist = float.MaxValue;

        foreach (var enemy in EnemyManager.instance.ActiveEnemies)
        {
            if (enemy == null) continue;

            float dist =
                Vector3.Distance(transform.position, enemy.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }
}
