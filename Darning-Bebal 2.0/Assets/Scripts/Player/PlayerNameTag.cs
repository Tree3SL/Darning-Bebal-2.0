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
    private GameObject player;

    private void Start()
    {
        game = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = transform.parent.parent.gameObject;
        SetUpNameTag();
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

        if (player != null) 
        {
            player.GetComponent<PlayerManager>().team_index = color+1;
        }

        nameTag.text = photonView.Owner.NickName;
        nameTag.color = game.colors[color];

        if (photonView.IsMine)
        {
            nameTag.text += " (You)";
        }

    }
}
