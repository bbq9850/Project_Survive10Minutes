using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCore))]
[RequireComponent(typeof(PlayerMovement_QuarterView))]
[RequireComponent(typeof(PlayerStamina))]
public class PlayerDash : MonoBehaviour
{
    PlayerCore core;
    PlayerMovement_QuarterView movement;
    PlayerStamina stamina;

    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float staminaCost = 20f;

    [SerializeField] GameObject dashEffectPrefab;
    [SerializeField] float effectOffset = 0.8f;

    bool isDashing;
    bool canDash = true;
    void Awake()
    {
        core = GetComponent<PlayerCore>();
        movement = GetComponent<PlayerMovement_QuarterView>();
        stamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryDash();
        }
    }

    void TryDash()
    {
        if (!canDash) return;
        if (movement.MoveDir == Vector3.zero) return;

        if (!stamina.TryUse(staminaCost))
            return;

        StartCoroutine(DashRoutine());
        PlayDashEffect();
    }

    IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;

        float timer = 0f;
        Vector3 dashDir = movement.MoveDir.normalized;

        while (timer < dashDuration)
        {
            core.Controller.Move(dashDir * dashSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        canDash = true;
    }

    void PlayDashEffect()
    {
        if (dashEffectPrefab == null) return;
        if (movement == null) return;

        Vector3 dir = movement.MoveDir;

        if (dir == Vector3.zero)
            dir = transform.forward;

        dir.Normalize();

        Vector3 spawnPos = transform.position - dir * effectOffset;
        spawnPos.y = transform.position.y - 0.5f;

        Quaternion rot = Quaternion.LookRotation(-dir);

        GameObject fx = Instantiate(dashEffectPrefab, spawnPos, rot);

        Destroy(fx, 1.5f);
    }
    public bool IsDashing => isDashing;
}
