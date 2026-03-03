using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickUp : MonoBehaviour
{
    [SerializeField] float healAmount = 30f;

    HealPickupPool pool;

    public void SetPool(HealPickupPool pool)
    {
        this.pool = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerHealth health))
            return;

        health.PlayerHeal(healAmount);

        pool.ReturnToPool(this);
    }
}
