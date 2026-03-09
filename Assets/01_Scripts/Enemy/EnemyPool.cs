using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    
    [SerializeField] int initialSize = 30;

    [SerializeField] Transform activeParent;
    [SerializeField] Transform pooledParent;

    Dictionary<EnemyCore, Queue<EnemyCore>> pools
        = new Dictionary<EnemyCore, Queue<EnemyCore>>();

    void Awake()
    {
        Instance = this;
        
    }

    
    EnemyCore CreateNewEnemy(EnemyCore prefab)
    {
        EnemyCore enemy = Instantiate(prefab, pooledParent);
        enemy.SetOriginalPrefab(prefab); 
        enemy.gameObject.SetActive(false);
        return enemy;
    }
    public EnemyCore Get(EnemyCore prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new Queue<EnemyCore>();

            for (int i = 0; i < initialSize; i++)
            {
                pools[prefab].Enqueue(CreateNewEnemy(prefab));
            }
        }

        var pool = pools[prefab];

        if (pool.Count == 0)
            pool.Enqueue(CreateNewEnemy(prefab));

        EnemyCore enemy = pool.Dequeue();

        enemy.transform.SetParent(activeParent);
        enemy.gameObject.SetActive(true);

        return enemy;
    }

    public void ReturnToPool(EnemyCore enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(pooledParent);

        EnemyCore prefabKey = enemy.OriginalPrefab;
        pools[prefabKey].Enqueue(enemy);
    }
}
