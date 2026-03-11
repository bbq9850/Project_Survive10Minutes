using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected PlayerStat stat;

    protected float timer;

    protected int level = 1;
    public int Level => level;

    public int maxLevel = 5;

    [SerializeField] protected float baseAttackRate = 1f;
    public WeaponData Data { get; private set; }

    public void Init(WeaponData data)
    {
        Data = data;
    }

    public virtual void LevelUp(float value)
    {
        level++;
    }

    public bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

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
