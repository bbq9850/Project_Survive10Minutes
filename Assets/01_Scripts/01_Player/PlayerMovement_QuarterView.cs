using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement_QuarterView : MonoBehaviour
{

    [SerializeField] Transform mainCam;

    PlayerInput playerInput;
    PlayerCore playerCore;
    PlayerStat stat;
    PlayerDash dash;

    Vector3 camForward;
    Vector3 camRight;

    public Vector3 MoveDir { get; private set; }

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCore = GetComponent<PlayerCore>();
        stat = GetComponent<PlayerStat>();
        dash = GetComponent<PlayerDash>();

        if (mainCam == null)
            mainCam = Camera.main.transform;
    }

    void Update()
    {
        UpdateCameraDir();
        Move();
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos = MapBounds.Instance.ClampPosition(pos);

        pos.y = 1f;

        transform.position = pos;

        MapBounds.Instance.UpdateWalls(pos);
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
        if (dash != null && dash.IsDashing)
        {
            return;
        }
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

        Vector3 velocity = MoveDir * stat.moveSpeed;

        playerCore.Controller.Move(
            velocity * Time.deltaTime
        );
    }

}
