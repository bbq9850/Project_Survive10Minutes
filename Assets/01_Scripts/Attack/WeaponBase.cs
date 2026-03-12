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

    protected float weaponPower;
    protected float weaponRate;
    protected float weaponRange;

    protected int projectileCount = 1;

    bool isInitialized = false;
    public WeaponData Data { get; private set; }

    public void Init(WeaponData data, PlayerStat playerStat)
    {
        Data = data;
        stat = playerStat;

        ApplyLevelStat();

        isInitialized = true;
    }
    void ApplyLevelStat()
    {
        WeaponLevelData levelData = Data.levels[level - 1];

        weaponPower = levelData.weaponPower;
        weaponRate = levelData.weaponRate;
        weaponRange = levelData.weaponRange;
        projectileCount = levelData.projectileCount;
    }

    public virtual void LevelUp(float value)
    {
        if (level >= Data.levels.Length)
            return;

        level++;

        ApplyLevelStat();
    }

    public bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

    protected virtual void Update()
    {
        if (!isInitialized) return;

        timer += Time.deltaTime;

        float interval = 1f / (stat.attackSpeed * weaponRate);

        if (timer >= interval)
        {
            timer = 0f;
            Attack();
        }
    }

    protected abstract void Attack();
}
