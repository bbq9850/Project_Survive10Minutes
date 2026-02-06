using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public static EnemyManager instance;

    public List<EnemyCore> ActiveEnemies = new List<EnemyCore>();

    [SerializeField] EnemyPool enemyPool;
    [SerializeField] Transform player;
    [SerializeField] EnemyData enemyBaseData;

    [SerializeField] float spawnRadiusMin = 8f;
    [SerializeField] float spawnRadiusMax = 12f;

   

    float spawnInterval = 1.0f;
    int maxEnemyCount = 90;

    float timeElapsed;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    IEnumerator SpawnRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnInterval);

        while (true)
        {
            if (ActiveEnemies.Count < maxEnemyCount)
                SpawnEnemy();

            yield return wait;
        }
    }

    void SpawnEnemy()
    {
        EnemyCore enemy = enemyPool.Get();

        Vector3 spawnPos = RandomSpawnPos();
        enemy.transform.position = spawnPos;

        float minute = timeElapsed / 60f;
        float hpMultiplier = Mathf.Pow(1.18f, minute);

        EnemyData runtimeData = Instantiate(enemyBaseData);
        runtimeData.maxHP *= hpMultiplier;

        enemy.OnActiveEnemy(runtimeData, player);
        ActiveEnemies.Add(enemy);
    }

    public void OnEnemyDead(EnemyCore enemy)
    {
        ActiveEnemies.Remove(enemy);
        // EXP 지급 여기서
    }

    Vector3 RandomSpawnPos()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float dis = Random.Range(spawnRadiusMin, spawnRadiusMax);

        Vector3 offset = new Vector3(randomDir.x, 0, randomDir.y) * dis;
        return player.position + offset;
    }

    //void OnDrawGizmosSelected()
    //{
    //    if (player == null) return;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(player.position, spawnRadiusMin);

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(player.position, spawnRadiusMax);
    //}

}
