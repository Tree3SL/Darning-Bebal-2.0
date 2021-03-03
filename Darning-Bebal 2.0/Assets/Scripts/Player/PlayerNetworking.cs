using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerNetworking : MonoBehaviourPun
{
    /* ToDo: Need to assign player to team */

    public MonoBehaviour[] playerScripts;
    public Camera playerCamera;
    public CinemachineVirtualCamera cinemachine;
    public AudioListener audioListener;
    public BoxCollider2D boxCollider;
    public CircleCollider2D circleCollider;
    public Rigidbody2D rigidBody;
    public GameObject PlayerUiPrefab;

    //private PhotonView photonView;

    void Start()
    {
        //photonView = GetComponent<PhotonView>();

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

        if (PlayerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(PlayerUiPrefab);

            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player prefab.", this);
        }
    }


    /*private void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded();
    }


    private void CalledOnLevelWasLoaded()
    {
        // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone

        /*if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }*/

        /*GameObject _uiGo = Instantiate(this.PlayerUiPrefab);

        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }*/

}
