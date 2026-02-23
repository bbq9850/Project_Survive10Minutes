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

    [Header("Elite")]
    [SerializeField] EnemyData[] eliteDatas;

    bool elite3Spawned;
    bool elite6Spawned;
    bool elite9Spawned;

    [Header("Boss")]
    [SerializeField] EnemyData bossData;
    [SerializeField] float bossSpawnTime = 600f;
    [SerializeField] Transform bossSpawnPoint;

    private bool isBossSpawned;

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

        if(!isBossSpawned && timeElapsed >= bossSpawnTime)
        {
            SpawnBoss();
            isBossSpawned = true;
        }
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

    IEnumerator EliteSpawnRoutine()
    {
        while (true)
        {
            float minute = GameTimer.Instance.GetMinutes();

            if (!elite3Spawned && minute >= 3f)
            {
                SpawnElite(0);
                elite3Spawned = true;
            }

            if (!elite6Spawned && minute >= 6f)
            {
                SpawnElite(1);
                elite6Spawned = true;
            }

            if (!elite9Spawned && minute >= 9f)
            {
                SpawnElite(2);
                elite9Spawned = true;
            }

            yield return null;
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

    void SpawnElite(int index)
    {
        if (eliteDatas.Length <= index) return;

        EnemyData data = eliteDatas[index];

        Vector3 spawnPos = RandomSpawnPos();

        EnemyCore enemy = EnemyPool.Instance.Get();
        enemy.transform.position = spawnPos;
        enemy.OnActiveEnemy(data, player);

        Debug.Log($"Elite Spawned");
    }

    public void OnEnemyDead(EnemyCore enemy)
    {
        ActiveEnemies.Remove(enemy);
    }

    Vector3 RandomSpawnPos()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float dis = Random.Range(spawnRadiusMin, spawnRadiusMax);

        Vector3 offset = new Vector3(randomDir.x, 0, randomDir.y) * dis;
        return player.position + offset;
    }

    void SpawnBoss()
    {
        EnemyCore boss = enemyPool.Get();

        Vector3 spawnPos;
        if(bossSpawnPoint != null)
        {
            spawnPos = bossSpawnPoint.position;
        }
        else
        {
            spawnPos = RandomSpawnPos();
        }

        boss.transform.position = spawnPos;

        EnemyData runtimeData = Instantiate(bossData);

        boss.OnActiveEnemy(runtimeData, player);
        ActiveEnemies.Add(boss);
        boss.name = "Boss";
    }

}
