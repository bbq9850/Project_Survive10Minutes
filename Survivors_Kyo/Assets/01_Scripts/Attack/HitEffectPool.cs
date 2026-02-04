using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectPool : MonoBehaviour
{

    public static HitEffectPool Instance;

    [SerializeField] HitEffect effectPrefab;
    [SerializeField] int initialSize = 20;

    Queue<HitEffect> pool = new Queue<HitEffect>();

    void Awake()
    {
        Instance = this;
        Initialize();
    }

    void Initialize()
    {
        for (int i = 0; i < initialSize; i++)
            CreateNew();
    }

    HitEffect CreateNew()
    {
        HitEffect effect = Instantiate(effectPrefab, transform);
        effect.Init(this);
        effect.gameObject.SetActive(false);
        pool.Enqueue(effect);
        return effect;
    }

    public void Play(Vector3 position)
    {
        if (pool.Count == 0)
            CreateNew();

        HitEffect effect = pool.Dequeue();
        effect.Play(position);
    }

    public void Return(HitEffect effect)
    {
        effect.gameObject.SetActive(false);
        pool.Enqueue(effect);
    }
}
