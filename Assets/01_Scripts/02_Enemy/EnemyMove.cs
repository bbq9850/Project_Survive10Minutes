using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;

    [SerializeField] float separationRadius = 1.5f;
    [SerializeField] float pushForce = 1.2f;

    public void Init(float speed, Transform target)
    {
        this.moveSpeed = speed;
        this.target = target;
    }

    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        dir.Normalize();

        Vector3 move = dir;

        Collider[] hits = Physics.OverlapSphere(transform.position, separationRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            if (hit.TryGetComponent(out EnemyCore enemy))
            {
                Vector3 away = transform.position - enemy.transform.position;
                away.y = 0;

                float dist = away.magnitude;

                if (dist > 0 && dist < separationRadius)
                {
                    float push = (separationRadius - dist) / separationRadius;
                    move += away.normalized * push * pushForce;
                }
            }
        }

        move.Normalize();

        transform.position += move * moveSpeed * Time.deltaTime;

        if (move != Vector3.zero)
            transform.forward = move;
    }
}
