using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_QuarterViewRot : MonoBehaviour
{
    public static Camera_QuarterViewRot Instance;

    public Transform target;

    public Vector3 offset = new Vector3(0f, 10f, -10f);
    public float followSpeed = 8f;

    public float mouseRotateSpeed = 120f;
    public float returnSpeed = 5f;
    public float maxYawOffset = 30f;

    float currentYawOffset = 0f;
    float defaultYaw;

    Vector3 shakeOffset;
    Coroutine shakeCoroutine;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        defaultYaw = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        HandleMouseRotation();
        FollowTarget();
    }

    void HandleMouseRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            currentYawOffset += mouseX * mouseRotateSpeed * Time.deltaTime;
            currentYawOffset = Mathf.Clamp(currentYawOffset, -maxYawOffset, maxYawOffset);
        }
        else
        {
            currentYawOffset = Mathf.Lerp(
                currentYawOffset,
                0f,
                returnSpeed * Time.deltaTime
            );
        }
    }

    void FollowTarget()
    {
        Quaternion rot = Quaternion.Euler
        (
            45f,
            defaultYaw + currentYawOffset,
            0f
        );

        Vector3 desiredPos = target.position + offset + shakeOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            followSpeed * Time.deltaTime
        );

        transform.rotation = rot;
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float damper = 1f - (elapsed / duration);

            float x = Random.Range(-1f, 1f) * magnitude * damper;
            float y = Random.Range(-1f, 1f) * magnitude * damper;

            shakeOffset = new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeOffset = Vector3.zero;
    }
}
