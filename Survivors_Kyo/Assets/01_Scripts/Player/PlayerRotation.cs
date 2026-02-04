using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement_QuarterView))]
public class PlayerRotation : MonoBehaviour
{
    public float rotateSpeed = 12f;

    PlayerMovement_QuarterView movement;

    void Awake()
    {
        movement = GetComponent<PlayerMovement_QuarterView>();
    }

    void LateUpdate()
    {
        Vector3 dir = movement.MoveDir;

        if (dir.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );
    }
}
