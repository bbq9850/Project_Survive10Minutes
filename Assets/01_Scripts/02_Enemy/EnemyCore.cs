using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{

    public EnemyData data;
    public Transform target;

    EnemyHealth enemyHealth;
    EnemyMove enemyMove;
    EnemyExpDrop enemyExpDrop;

    [SerializeField] GameObject magnetPrefab;
    [SerializeField] float magnetDropChance = 1f;

    bool isDead;

    public EnemyCore OriginalPrefab { get; private set; }
    public void SetOriginalPrefab(EnemyCore prefab)
    {
        OriginalPrefab = prefab;
    }

    public int EnemyNum {  get; private set; }
    static int nextNum = 0;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMove = GetComponent<EnemyMove>();
        enemyExpDrop = GetComponent<EnemyExpDrop>();

        EnemyNum = ++nextNum;
        gameObject.name = $"Enemy_{EnemyNum:D2}";
    }

    public void OnActiveEnemy(EnemyData data, Transform target)
    {
        this.data = data;
        this.target = target;

        enemyHealth.Init(data.maxHP, data);
        enemyMove.Init(data.moveSpeed, target);

        isDead = false;
        gameObject.SetActive(true);
    }

    public void OnDeadEnemy()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        GameManager.Instance.AddKill();

        if (data != null)
        {
            ExpOrbPool.Instance.Spawn(
                transform.position, data.expValue);

            TryDropHeal();
            TryDropMagnet();
        }

        EnemyManager.instance.OnEnemyDead(this);
        data = null;
        EnemyPool.Instance.ReturnToPool(this);
    }

    void TryDropHeal()
    {
        if (data == null) { return; }

        if (Random.value <= data.healDropChance)
        {
            HealPickupPool.Instance.Spawn(transform.position);
        }
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

    void TryDropMagnet()
    {
        if (data == null) return;

        if (!data.isElite) return;

        if (Random.value <= magnetDropChance)
        {
            Vector3 pos = transform.position;
            pos.y = 0.5f;
            Instantiate(magnetPrefab, pos, Quaternion.identity);
        }
    }

}
