using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    int expAmount;
    ExpOrbPool pool;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float attractDistance = 2f;
    [SerializeField] float collectDistance = 0.5f;

    bool isActive;

    public void Init(ExpOrbPool pool)
    {
        this.pool = pool;
    }

    

    void Update()
    {
        if (!isActive)
            return;

        Transform player = PlayerCore.Instance.transform;

        if (player == null)
            return;

        Vector3 dir = player.position - transform.position;
        float dist = dir.magnitude;

        if (dist <= collectDistance)
        {
            Collect();
            return;
        }

        if (dist <= attractDistance)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position += dir.normalized * step;
        }
    }

    void Collect()
    {
        if (!isActive) return;

        isActive = false;

        PlayerExp.Instance.AddExp(expAmount);

        pool.Return(this);
    }
    public void Activate(Vector3 position, int amount)
    {
        transform.position = position;
        expAmount = amount;

        isActive = true;
        gameObject.SetActive(true);
        
    }
    public void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
}
