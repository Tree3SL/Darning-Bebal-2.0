using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float walkSpeed = 15f;

    private float move = 0f;
    private bool jump = false;
    private bool stun = false;
    private bool fire2Pressed = false;
    private bool fire1Pressed = false;

    private void Update()
    {
        move = Input.GetAxisRaw("Horizontal") * walkSpeed;
        animator.SetFloat("speed", Mathf.Abs(move));

        if(Input.GetButtonDown("Fire2"))
        {
            fire2Pressed = true;
            stun = true;
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            fire1Pressed = true;
            stun = false;
        }
        else if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("jump", true);
        }
    }

    private void FixedUpdate()
    { 
        if(fire2Pressed)
        {
            fire2Pressed = false;
            animator.SetBool("isStunned", stun);
        }
        else if(fire1Pressed)
        {
            fire1Pressed = false;
            animator.SetBool("isUp", true);
        }

        //animator.SetFloat("speed", Mathf.Abs(move));

        if (!stun)
        {
            //animator.SetBool("jump", true);
            controller.Move(move * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }

    private void OnStun()
    {
        animator.SetBool("isStunned", false);
    }

    private void OnUp()
    {
        if(animator.GetBool("isUp"))
        {
            animator.SetBool("isUp", false);
        }
    }

    public void OnJump()
    {
        animator.SetBool("jump", false);
    }
}
