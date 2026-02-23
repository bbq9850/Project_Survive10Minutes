using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]Animator animator;
    PlayerMovement_QuarterView movement;

    void Awake()
    {
        
        movement = GetComponent<PlayerMovement_QuarterView>();
    }

    void Update()
    {
        UpdateMoveAnimation();
    }

    void UpdateMoveAnimation()
    {
        float moveAmount = movement.MoveDir.magnitude;

        animator.SetFloat("Move", moveAmount);
        
    }
}
