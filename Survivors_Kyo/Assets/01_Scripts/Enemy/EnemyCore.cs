using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{

    public EnemyData data;
    public Transform target;

    EnemyHealth enemyHealth;
    EnemyMove enemyMove;

    public int EnemyNum {  get; private set; }
    static int nextNum = 0;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMove = GetComponent<EnemyMove>();

        EnemyNum = ++nextNum;
        gameObject.name = $"Enemy_{EnemyNum:D2}";
    }

    public void OnActiveEnemy(EnemyData data, Transform target)
    {
        this.data = data;
        this.target = target;

        enemyHealth.Init(data.maxHP);
        enemyMove.Init(data.moveSpeed, target);

        gameObject.SetActive(true);
        
    }

    public void OnDeadEnemy()
    {
        EnemyManager.instance.OnEnemyDead(this);
        data = null;
        EnemyPool.Instance.ReturnToPool(this);
    }

    public void TakeDamage(float damage)
    {
        if(enemyHealth == null)
        {
            Debug.Log($"{name} health is null");
            return;
        }
        enemyHealth.TakeDamage(damage);
    }

}
