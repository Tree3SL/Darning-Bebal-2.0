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
    [HideInInspector]
    public bool playerUp = false;

    private float move = 0f;
    private bool jump = false;
    //private bool stun = false;
    //private bool fire2Pressed = false;
    //private bool fire1Pressed = false;

    private void Update()
    {
        move = Input.GetAxisRaw("Horizontal") * walkSpeed * boostMult;
        animator.SetFloat("speed", Mathf.Abs(move));

        /*if(Input.GetButtonDown("Fire2"))
        {
            fire2Pressed = true;
            stun = true;
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            fire1Pressed = true;
            stun = false;
        }*/
        
        if(stun)
        {
            animator.SetBool("isStunned", true);
            stun = false;
        }
        //else if(playerUp)
        else if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    { 
        /*if(fire2Pressed)
        {
            fire2Pressed = false;
            animator.SetBool("isStunned", stun);
        }
        else if(fire1Pressed)
        {
            fire1Pressed = false;
            animator.SetBool("isUp", true);
        }*/

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
}
