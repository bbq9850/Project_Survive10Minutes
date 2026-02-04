using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    int expAmount;
    Transform player;
    ExpOrbPool pool;

    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float attractDistance = 2.5f;

    public void Init(ExpOrbPool pool, Transform player)
    {
        this.pool = pool;
        this.player = player;
    }

    public void Activate(Vector3 position, int amount)
    {
        transform.position = position;
        expAmount = amount;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attractDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<PlayerExp>().AddExp(expAmount);
        pool.Return(this);
    }
}
