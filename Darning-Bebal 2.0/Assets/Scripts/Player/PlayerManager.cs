using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPun
{
    private float direction = 1f;
    private PlayerMovement movement;

    public int team_index;  //index from color+1, result in 1 or 2

    private void Start()
    {
        movement = gameObject.GetComponent<PlayerMovement>();
        if (photonView.IsMine) 
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().save_player(this.gameObject);
        }
            
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
        //movement.playerRigidbody.isKinematic = true;
    }

    public void EnablePlayer()
    {
        //movement.controlsEnabled = true;
        movement.stun = false;
        movement.EnableAnimations();
        movement.controlsEnabled = true;
    }
}
