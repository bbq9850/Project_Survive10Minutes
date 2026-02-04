using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{

    public float maxHP;
    public float moveSpeed;
    public int attackDamage;
    public int expValue = 1;

}
