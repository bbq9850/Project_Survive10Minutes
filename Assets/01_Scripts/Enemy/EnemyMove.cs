using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;

    [SerializeField] float separationRadius = 0.7f;
    [SerializeField] float separationForce = 2f;

    public void Init(float speed, Transform target)
    {
        this.moveSpeed = speed;
        this.target = target;
    }
    private void Update()
    {
        EnemyMovement();
    }
    public void EnemyMovement()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0;

        Vector3 moveDir = dir.normalized;

        Collider[] hits = Physics
            .OverlapSphere(transform.position, separationRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            if (hit.TryGetComponent(out EnemyCore enemy))
            {
                Vector3 away = transform.position - enemy.transform.position;
                away.y = 0;

                moveDir += away.normalized * separationForce;
            }
        }

        moveDir.Normalize();

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if (moveDir != Vector3.zero)
            transform.forward = moveDir;
    }
}
