using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [SerializeField] EnemyCore enemyPrefab;
    [SerializeField] int initialSize = 30;

    [SerializeField] Transform activeParent;
    [SerializeField] Transform pooledParent;

    Queue<EnemyCore> pool = new Queue<EnemyCore>();

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewEnemy();
        }
    }

    EnemyCore CreateNewEnemy()
    {
        EnemyCore enemy = Instantiate(enemyPrefab, pooledParent);
        enemy.gameObject.SetActive(false);
        pool.Enqueue(enemy);
        return enemy;
    }

    public EnemyCore Get()
    {
        if (pool.Count == 0)
            CreateNewEnemy();

        EnemyCore enemy = pool.Dequeue();
        enemy.transform.SetParent(activeParent);
        return enemy;
    }

    public void ReturnToPool(EnemyCore enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(pooledParent);
        pool.Enqueue(enemy);
    }
}
