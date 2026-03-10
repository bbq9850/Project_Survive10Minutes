using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 direction;
    float damage;

    public float speed = 6f;

    public void Init(Vector3 dir, float dmg)
    {
        direction = dir;
        damage = dmg;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyCore enemy = other.GetComponent<EnemyCore>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            ProjectilePool.Instance.Return(gameObject);
        }
    }
}
