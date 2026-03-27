using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 direction;
    float damage;

    public float speed = 6f;
    float lifeTime = 7f;
    float timer;

    [SerializeField] GameObject magicEffect;

    public void Init(Vector3 dir, float dmg)
    {
        direction = dir.normalized;
        damage = dmg;
        timer = 0;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timer += Time.deltaTime;

        if(timer > lifeTime)
        {
            ProjectilePool.Instance.Return(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyCore enemy = other.GetComponent<EnemyCore>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            GameObject fx = Instantiate(magicEffect, transform.position, Quaternion.identity);
            Destroy(fx, 0.3f);

            ProjectilePool.Instance.Return(gameObject);
        }
    }
}
