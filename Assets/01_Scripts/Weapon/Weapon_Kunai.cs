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

        Vector3 baseDir =
            (target.transform.position - transform.position).normalized;
        baseDir.y = 0;

        for (int i = 0; i < projectileCount; i++)
        {
            float angleOffset = (i - (projectileCount - 1) / 2f) * 10f;

            Vector3 dir =
                Quaternion.Euler(0, angleOffset, 0) * baseDir;

            GameObject kunai = ProjectilePool.Instance.Get();

            kunai.transform.position = transform.position;

            kunai.transform.rotation =
                Quaternion.LookRotation(dir) * Quaternion.Euler(90, 0, 0);

            float damage = stat.attackPower * weaponPower;

            kunai.GetComponent<Projectile>()
                .Init(dir, damage);
        }
    }

    EnemyCore FindClosestEnemy()
    {
        EnemyCore closest = null;
        float minDist = float.MaxValue;

        foreach (var enemy in EnemyManager.instance.ActiveEnemies)
        {
            if (enemy == null) continue;
            if (!enemy.gameObject.activeInHierarchy) continue;

            float dist =
                Vector3.Distance(transform.position, enemy.transform.position);

            if (dist < minDist && dist <= weaponRange)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }
}

