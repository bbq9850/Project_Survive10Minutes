using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected PlayerStat stat;

    protected float timer;

    [SerializeField] protected float baseAttackRate = 1f;

    protected virtual void Awake()
    {
        stat = GetComponentInParent<PlayerStat>();
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        float interval = 1f / (stat.attackSpeed * baseAttackRate);

        if (timer >= interval)
        {
            timer = 0f;
            Attack();
        }
    }

    protected abstract void Attack();
}
