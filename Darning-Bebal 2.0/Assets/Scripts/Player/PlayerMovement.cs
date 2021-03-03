using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float walkSpeed = 15f;
    [HideInInspector]
    public float boostMult = 1f;
    public bool controlsEnabled = true;
    //[HideInInspector]
    public bool stun = false;
    public GameObject target;
    public bool isIn = false;

    private float move = 0f;
    private bool jump = false;
    public bool stunAnimation = false;
    public bool playerUp = false;
    public Rigidbody2D playerRigidbody;

    private void Update()
    {
        move = Input.GetAxisRaw("Horizontal") * walkSpeed * boostMult;

        if (controlsEnabled)
        {
            animator.SetFloat("speed", Mathf.Abs(move));

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jump = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && isIn)
            {
                this.transform.localPosition = target.transform.localPosition;
            }
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
            playerUp = false;
        }
    }

    private void FixedUpdate()
    { 
        if (controlsEnabled)
        {
            controller.Move(move * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
        else
        {
            playerRigidbody.velocity = Vector3.zero;
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
            //playerUp = false;
        }
    }

    public void TriggerStunAnimation()
    {
        stunAnimation = true;
    }

    public void EnableAnimations()
    {
        playerUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            switch (collision.GetComponent<Door>().GetDoorEnum())
            {
                case Door.DoorEnum.Left:
                    target = collision.GetComponent<Door>().GetObjEnd();
                    isIn = true;
                    break;
                case Door.DoorEnum.Right:
                    target = collision.GetComponent<Door>().GetObjEnd();
                    isIn = true;
                    break;
                default:
                    break;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            switch (collision.GetComponent<Door>().GetDoorEnum())
            {
                case Door.DoorEnum.Left:
                    target = collision.GetComponent<Door>().GetObjEnd();
                    isIn = true;
                    break;
                case Door.DoorEnum.Right:
                    target = collision.GetComponent<Door>().GetObjEnd();
                    isIn = true;
                    break;
                default:
                    break;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            target = null;
            isIn = false;
        }
    }
}
