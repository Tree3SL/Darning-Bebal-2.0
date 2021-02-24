using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{ 
    // disable controls
    // enable controls
    //public float speedBoost = 2.5f;

    //private SpriteRenderer sprite;
    private float direction = 1f;
    private PlayerMovement movement;

    private void Start()
    {
        //sprite = gameObject.GetComponent<SpriteRenderer>();
        movement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float currentAxis = Input.GetAxisRaw("Horizontal");

        if(currentAxis != 0 && currentAxis != direction)
        {
            direction = currentAxis;
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            DisablePlayer(true);
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            EnablePlayer();
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
        }

        movement.controlsEnabled = false;
    }

    public void EnablePlayer()
    {
        movement.controlsEnabled = true;
    }
}
