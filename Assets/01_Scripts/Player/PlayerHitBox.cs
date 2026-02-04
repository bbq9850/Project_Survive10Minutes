using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerInvincible playerInvincible;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerInvincible = GetComponent<PlayerInvincible>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerInvincible.IsInvicible) return;

        if(other.TryGetComponent(out EnemyDamage enemyDamage))
        {
            playerHealth.TakeDamage(enemyDamage.Damage);
            playerInvincible.Activate();
        }
    }

    void Update()
    {
        
    }
}
