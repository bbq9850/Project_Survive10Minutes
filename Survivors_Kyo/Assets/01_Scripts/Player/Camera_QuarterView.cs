using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_QuarterView : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 10f, -10f);
    public float followSpeed = 8f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
        );

        transform.LookAt(target.position);
    }
}
