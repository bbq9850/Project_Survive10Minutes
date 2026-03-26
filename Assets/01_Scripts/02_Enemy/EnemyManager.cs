using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public static EnemyManager instance;

    public List<EnemyCore> ActiveEnemies { get; private set; } 
        = new List<EnemyCore>();

    [SerializeField] EnemyPool enemyPool;
    [SerializeField] Transform player;
    //[SerializeField] EnemyData enemyBaseData;
    [SerializeField] List<EnemyData> enemyDatas;

    [SerializeField] float spawnRadiusMin = 20f;
    [SerializeField] float spawnRadiusMax = 28f;

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

    private bool isBossSpawned;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(EliteSpawnRoutine());
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
        //WaitForSeconds wait = new WaitForSeconds(spawnInterval);

        while (true)
        {
            float spawnMul =
            EnemyDifficultySystem.Instance.GetSpawnMultiplier();

            float dynamicInterval =
            spawnInterval / spawnMul;

            int spawnCount = Mathf.RoundToInt(spawnMul);
            spawnCount = Mathf.Clamp(spawnCount, 1, 5);

            int dynamicMax = Mathf.RoundToInt(
                             maxEnemyCount 
                             * EnemyDifficultySystem.Instance.GetSpawnMultiplier()
                            );

            dynamicMax = Mathf.Clamp(dynamicMax, 90, 200);

            for (int i = 0; i < spawnCount; i++)
            {
                if (ActiveEnemies.Count >= dynamicMax)
                    break;

                SpawnEnemy();
            }

            yield return new WaitForSeconds(dynamicInterval);
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

        if (enemyDatas == null || enemyDatas.Count == 0)
            return;

        EnemyData baseData =
            enemyDatas[Random.Range(0, enemyDatas.Count)];

        EnemyCore enemy =
            EnemyPool.Instance.Get(baseData.enemyPrefab);

        Vector3 spawnPos = RandomSpawnPos();
        enemy.transform.position = spawnPos;

        float hpMul =
            EnemyDifficultySystem.Instance.GetHpMultiplier();

        float dmgMul =
            EnemyDifficultySystem.Instance.GetDamageMultiplier();

        EnemyData runtimeData = Instantiate(baseData);
        runtimeData.maxHP *= hpMul;
        runtimeData.attackDamage *= Mathf.RoundToInt(dmgMul);

        enemy.OnActiveEnemy(runtimeData, player);
        ActiveEnemies.Add(enemy);
    }

    void SpawnElite(int index)
    {
        if (eliteDatas.Length <= index) return;

        EnemyData data = eliteDatas[index];

        EnemyCore enemy = EnemyPool.Instance.Get(data.enemyPrefab);

        Vector3 spawnPos = RandomSpawnPos();
        enemy.transform.position = spawnPos;

        enemy.OnActiveEnemy(data, player);
        ActiveEnemies.Add(enemy);

        if (BossWarningUI.Instance != null)
        {
            BossWarningUI.Instance.ShowWarning("ELITE WARNING");
        }
        Debug.Log($"Elite Spawned");
    }

    public void OnEnemyDead(EnemyCore enemy)
    {
        ActiveEnemies.Remove(enemy);
    }

    Vector3 RandomSpawnPos()
    {
        Vector3 spawnPos;

        int tryCount = 0;

        do
        {
            Vector2 randomDir = Random.insideUnitCircle;
            float dis = Random.Range(spawnRadiusMin, spawnRadiusMax);

            Vector3 offset = new Vector3(randomDir.x, 0, randomDir.y) * dis;

            spawnPos = new Vector3(
                player.position.x + offset.x,
                0f,
                player.position.z + offset.z
            );

            tryCount++;

            if (tryCount > 10)
                break;

        } while (!MapBounds.Instance.IsInside(spawnPos));

        return MapBounds.Instance.ClampPosition(spawnPos);
    }

    void SpawnBoss()
    {
        EnemyCore boss = EnemyPool.Instance.Get(bossData.enemyPrefab);

        Vector3 spawnPos = RandomSpawnPos();
        boss.transform.position = spawnPos;

        EnemyData runtimeData = Instantiate(bossData);

        boss.OnActiveEnemy(runtimeData, player); 
        ActiveEnemies.Add(boss);
        boss.name = "Boss";

        if (BossWarningUI.Instance != null)
        {
            BossWarningUI.Instance.ShowWarning("BOSS WARNING");
        }
    }

}
