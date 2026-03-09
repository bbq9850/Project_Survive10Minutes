using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbPool : MonoBehaviour
{
    public static ExpOrbPool Instance;

    [SerializeField] ExpOrb expPrefab;
    [SerializeField] int initialSize = 100;
    [SerializeField] int maxSize = 300;
    [SerializeField] Transform player;

    [SerializeField] Transform expParent;

    Queue<ExpOrb> pool = new Queue<ExpOrb>();
    HashSet<ExpOrb> allOrbs = new HashSet<ExpOrb>();
    HashSet<ExpOrb> inactiveSet = new HashSet<ExpOrb>();

    void Awake()
    {
        Instance = this;

        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        for (int i = 0; i < initialSize; i++)
            CreateNew();

    }

    ExpOrb CreateNew()
    {
        if (allOrbs.Count >= maxSize)
            return null;

        ExpOrb orb = Instantiate(expPrefab);
        orb.Init(this);
        orb.gameObject.SetActive(false);
        orb.transform.SetParent(expParent);

        pool.Enqueue(orb);
        allOrbs.Add(orb);
        inactiveSet.Add(orb);

        return orb;
    }

    public void Spawn(Vector3 position, int amount)
    {
        ExpOrb orb = null;
        
        if (pool.Count > 0)
        {
            orb = pool.Dequeue();
            inactiveSet.Remove(orb);
        }
        else
        {
            orb = CreateNew();
        }

        if (orb == null)
            return;

        position.y = 0.5f;

        orb.Activate(position, amount);
    }

    public void Return(ExpOrb orb)
    {
        if (orb == null)
            return;

        if (!allOrbs.Contains(orb))
            return;

        if (inactiveSet.Contains(orb))
            return;

        orb.Deactivate();
        pool.Enqueue(orb);
        inactiveSet.Add(orb);
    }
}
