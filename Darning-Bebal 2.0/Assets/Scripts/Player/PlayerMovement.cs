using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* ToDo: Need to fix jumping animation */

    public CharacterController2D controller;
    public Animator animator;
    public float walkSpeed = 15f;
    [HideInInspector]
    public float boostMult = 1f;
    public bool controlsEnabled = true;
    [HideInInspector]
    public bool stun = false;

    private float move = 0f;
    private bool jump = false;
    private bool stunAnimation = false;
    private bool playerUp = false;

    private void Update()
    {
        move = Input.GetAxisRaw("Horizontal") * walkSpeed * boostMult;

        if (controlsEnabled)
        {
            animator.SetFloat("speed", Mathf.Abs(move));
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }
        
        if(stunAnimation)
        {
            animator.SetBool("isStunned", true);
            stunAnimation = false;
        }
        else if(playerUp)
        {
            animator.SetBool("isUp", true);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    { 
        if (controlsEnabled)
        {
            controller.Move(move * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }

    public void OnStun()
    {
        animator.SetBool("isStunned", false);
    }

    public void OnUp()
    {
        if(animator.GetBool("isUp"))
        {
            animator.SetBool("isUp", false);
        }
    }

    public void OnJump()
    {
        //animator.SetBool("jump", false);
    }

    public void TriggerStunAnimation()
    {
        stunAnimation = true;
    }

    public void EnableAnimations()
    {
        playerUp = true;
    }
}
