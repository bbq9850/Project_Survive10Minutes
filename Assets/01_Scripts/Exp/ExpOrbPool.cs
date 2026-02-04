using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbPool : MonoBehaviour
{
    public static ExpOrbPool Instance;

    [SerializeField] ExpOrb expPrefab;
    [SerializeField] int initialSize = 50;
    [SerializeField] Transform player;

    Queue<ExpOrb> pool = new Queue<ExpOrb>();

    void Awake()
    {
        Instance = this;
        Init();
    }

    void Init()
    {
        for (int i = 0; i < initialSize; i++)
            CreateNew();
    }

    ExpOrb CreateNew()
    {
        ExpOrb orb = Instantiate(expPrefab, transform);
        orb.Init(this, player);
        orb.gameObject.SetActive(false);
        pool.Enqueue(orb);
        return orb;
    }

    public void Spawn(Vector3 position, int amount)
    {
        if (pool.Count == 0)
            CreateNew();

        ExpOrb orb = pool.Dequeue();
        orb.Activate(position, amount);
    }

    public void Return(ExpOrb orb)
    {
        orb.gameObject.SetActive(false);
        pool.Enqueue(orb);
    }
}
