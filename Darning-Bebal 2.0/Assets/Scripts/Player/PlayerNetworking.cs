using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerNetworking : MonoBehaviour
{
    /* ToDo: Need to assign player to team */

    public MonoBehaviour[] playerScripts;
    public Camera playerCamera;
    public CinemachineVirtualCamera cinemachine;
    public AudioListener audioListener;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if(!photonView.IsMine)
        {
            foreach(MonoBehaviour script in playerScripts)
            {
                script.enabled = false;
                playerCamera.enabled = false;
                cinemachine.enabled = false;
                audioListener.enabled = false;
            }
        }
    }
}
