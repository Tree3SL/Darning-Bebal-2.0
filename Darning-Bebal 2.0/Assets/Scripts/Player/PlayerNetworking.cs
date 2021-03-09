using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerNetworking : MonoBehaviourPun
{
    public MonoBehaviour[] playerScripts;
    public Camera playerCamera;
    public CinemachineVirtualCamera cinemachine;
    public AudioListener audioListener;
    public BoxCollider2D boxCollider;
    public CircleCollider2D circleCollider;
    public Rigidbody2D rigidBody;
    
    void Start()
    {
        if(!photonView.IsMine)
        {
            foreach(MonoBehaviour script in playerScripts)
            {
                script.enabled = false;
                playerCamera.enabled = false;
                cinemachine.enabled = false;
                audioListener.enabled = false;
                rigidBody.isKinematic = true;
                boxCollider.enabled = false;
                circleCollider.enabled = false;
            }
        }
    }

}
