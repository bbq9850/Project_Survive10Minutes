using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;

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

        transform.position += dir.normalized * moveSpeed * Time.deltaTime;
        transform.forward = dir;
    }
}
