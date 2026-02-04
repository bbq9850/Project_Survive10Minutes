using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    public float Damage => damage;

    public void Init(int enemyDamage)
    {
        this.damage = enemyDamage;
    }

    

    
}
