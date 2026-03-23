using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickupPool : MonoBehaviour
{
    public static HealPickupPool Instance;

    [SerializeField] HealPickUp prefab;
    [SerializeField] int initialSize = 10;

    Queue<HealPickUp> pool = new Queue<HealPickUp>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < initialSize; i++)
        {
            CreateNew();
        }
    }

    HealPickUp CreateNew()
    {
        HealPickUp obj = Instantiate(prefab, transform);
        obj.gameObject.SetActive(false);
        obj.SetPool(this);
        pool.Enqueue(obj);
        return obj;
    }

    public void Spawn(Vector3 position)
    {
        if (pool.Count == 0)
        {
            CreateNew();
        }

        HealPickUp obj = pool.Dequeue();
        position.y = 0.5f;
        obj.transform.position = position;
        
        obj.gameObject.SetActive(true);
    }

    public void ReturnToPool(HealPickUp obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
