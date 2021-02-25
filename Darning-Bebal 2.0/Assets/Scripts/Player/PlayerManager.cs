using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{ 
    /*
     * Player slows when interacting with an object created by Alan.
     */

    private float direction = 1f;
    private PlayerMovement movement;

    private void Start()
    {
        movement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float currentAxis = Input.GetAxisRaw("Horizontal");

        if(currentAxis != 0 && currentAxis != direction)
        {
            direction = currentAxis;
        }
    }

    public float GetDirection()
    {
        return direction;
    }

    public void BoostSpeed(float boost)
    {
        movement.boostMult = boost;
    }

    public void ResetSpeed()
    {
        movement.boostMult = 1f;
    }

    public void DisablePlayer(bool stun)
    {
        if(stun)
        {
            movement.stun = true;
            movement.TriggerStunAnimation();
        }

        movement.controlsEnabled = false;
    }

    public void EnablePlayer()
    {
        movement.controlsEnabled = true;
        movement.stun = false;
        movement.EnableAnimations();
    }
}
