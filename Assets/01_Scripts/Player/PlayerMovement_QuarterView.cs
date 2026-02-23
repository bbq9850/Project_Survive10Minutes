using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement_QuarterView : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform mainCam;

    PlayerInput playerInput;
    PlayerCore playerCore;

    Vector3 camForward;
    Vector3 camRight;

    public Vector3 MoveDir { get; private set; }

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCore = GetComponent<PlayerCore>();

        if (mainCam == null)
            mainCam = Camera.main.transform;
    }

    void Update()
    {
        UpdateCameraDir();
        Move();
    }

    void UpdateCameraDir()
    {
        camForward = mainCam.forward;
        camRight = mainCam.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();
    }

    void Move()
    {
        Vector3 inputDir = playerInput.PlayerMoveInput;

        if (inputDir.sqrMagnitude < 0.001f)
        {
            MoveDir = Vector3.zero;
        }
        else
        {
            MoveDir = camForward * inputDir.z + camRight * inputDir.x;

            if (MoveDir.sqrMagnitude > 1f)
                MoveDir.Normalize();
        }

        Vector3 velocity = MoveDir * moveSpeed;

        playerCore.Controller.Move(
            velocity * Time.deltaTime
        );
    }

}
