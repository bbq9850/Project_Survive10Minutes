using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{

    public EnemyType enemyType;
    public float maxHP;
    public float moveSpeed;
    public int attackDamage;
    public int expValue;

    [Range(0f, 1f)]
    public float healDropChance;

    public float goldDropChance;
    public int dropGold;

    public EnemyCore enemyPrefab;

    public bool isElite;
    public bool isBoss;
}
