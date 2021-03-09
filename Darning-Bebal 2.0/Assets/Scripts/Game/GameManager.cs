﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{
    public Color[] colors;
    public List<string> team1;
    public List<string> team2;
    public Transform spawn;

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetUpTeams", RpcTarget.AllBuffered);
        }

        PhotonNetwork.Instantiate("Demo Player Photon", spawn.position, Quaternion.identity);
    }



    [PunRPC]
    public void SetUpTeams()
    {
        team1 = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties["Team1"];
        team2 = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties["Team2"];
        colors[0] = (Color)PhotonNetwork.CurrentRoom.CustomProperties["Color1"];
        colors[1] = (Color)PhotonNetwork.CurrentRoom.CustomProperties["Color2"];
    }
}
