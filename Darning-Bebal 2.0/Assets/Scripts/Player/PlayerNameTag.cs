using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField]
    private Text nameTag;

    private GameManager game;

    private void Start()
    {
        game = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if(!photonView.IsMine)
        {
            SetUpNameTag();
        }
    }

    public void SetUpNameTag()
    {
        int color;

        if(game.team1.Contains(photonView.Owner.NickName))
        {
            color = 0;
        }
        else
        {
            color = 1;
        }

        nameTag.text = photonView.Owner.NickName;
        nameTag.color = game.colors[color];

    }
}
