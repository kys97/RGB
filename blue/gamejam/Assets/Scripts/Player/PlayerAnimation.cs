using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    CharacterController2D controller;
    Animator animator;

    int crouchEndHash;
    int crouchStartHash;
    int isMovingHash;
    int isDashingHash;
    bool wasCrouching;
    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");
        //isAttackingHash = Animator.StringToHash("Attack");
        isDashingHash = Animator.StringToHash("Dash");
        crouchStartHash = Animator.StringToHash("Crouch_start");
        crouchEndHash = Animator.StringToHash("Crouch_end");
    }
    void Update()
    {
        /*if (controller.isAttacking)
        {
            animator.Play(isAttackingHash);
            controller.isAttacking = false;
        }*/
        if (controller.isMoving)
            animator.SetBool(isMovingHash, true);
        else
            animator.SetBool(isMovingHash, false);
        if (controller.isDashing)
        {
            animator.Play(isDashingHash);
            controller.isDashing = false;
        }

        if (controller.isCrouching && !wasCrouching)
        {
            wasCrouching = true;
            animator.SetTrigger(crouchStartHash);
        }
        else if(!controller.isCrouching && wasCrouching)
        {
            wasCrouching = false;
            animator.SetTrigger(crouchEndHash);
        }
    }
}
