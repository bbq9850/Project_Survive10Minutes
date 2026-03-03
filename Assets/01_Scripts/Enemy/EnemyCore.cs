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

    //[SerializeField] Transform visualRoot;

    //GameObject currentVisual;

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

        enemyHealth.Init(data.maxHP);
        enemyMove.Init(data.moveSpeed, target);

        //if (currentVisual == null && data.visualPrefab != null)
        //{
        //    currentVisual = 
        //        Instantiate(data.visualPrefab, target);
        //    currentVisual.transform.localPosition = Vector3.zero;
        //    currentVisual.transform.localRotation = Quaternion.identity;
        //}

        gameObject.SetActive(true);
    }

    public void OnDeadEnemy()
    {
        KillCountManager.Instance.AddKill();
        
        if(data != null)
        {
            ExpOrbPool.Instance.Spawn(
                transform.position, data.expValue);

            TryDropHeal();
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

}
