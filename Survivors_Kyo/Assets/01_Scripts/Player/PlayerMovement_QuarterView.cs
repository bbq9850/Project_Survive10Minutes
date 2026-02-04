using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement_QuarterView : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform mainCam;

    public Vector3 MoveDir 
    { 
        get; 
        private set; 
    }

    PlayerInput playerInput;

    Vector3 camForward;
    Vector3 camRight;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (mainCam == null)
            mainCam = Camera.main.transform;

    }

    void Update()
    {
        CameraDir();
        Move();
    }

    void CameraDir()
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
            return;
        }

        MoveDir = camForward * inputDir.z + camRight * inputDir.x;
        if (MoveDir.sqrMagnitude > 1f)
            MoveDir.Normalize();

        transform.position += MoveDir * moveSpeed * Time.deltaTime;
    }
  
}
