using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    int expAmount;
    ExpOrbPool pool;

    bool isMagnet;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float attractDistance = 2f;
    [SerializeField] float collectDistance = 0.5f;

    bool isActive;

    Transform player;

    public void Init(ExpOrbPool pool)
    {
        this.pool = pool;
        player = PlayerCore.Instance.transform;
    }

    

    void Update()
    {
        if (!isActive || player == null)
            return;

        Vector3 dir = player.position - transform.position;
        float dist = dir.magnitude;

        if (isMagnet)
        {
            float step = moveSpeed * 2f * Time.deltaTime;
            transform.position += dir.normalized * step;

            if (dist <= collectDistance)
            {
                Collect();
            }
            return;
        }

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

        isMagnet = false;

        isActive = true;
        gameObject.SetActive(true);
        
    }
    public void Deactivate()
    {
        isActive = false;
        isMagnet = false;
        gameObject.SetActive(false);
    }

    public void StartMagnet()
    {
        isMagnet = true;
    }
}
