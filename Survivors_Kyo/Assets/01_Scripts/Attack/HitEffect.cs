using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    ParticleSystem ps;
    HitEffectPool pool;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Init(HitEffectPool pool)
    {
        this.pool = pool;
    }

    public void Play(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);

        ps.Play();
        StartCoroutine(ReturnAfter());
    }

    IEnumerator ReturnAfter()
    {
        yield return new WaitForSeconds(ps.main.duration);
        pool.Return(this);
    }
}
