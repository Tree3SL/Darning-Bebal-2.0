using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    /* ToDo: Need to make Lobby Gui */

    public int maxPlayers;
    public string roomName;
    public GameObject spawn;
    public GameManager game;
    /*[HideInInspector]
    public TeamManager[] teams;

    public int numberOfTeams = 2;
    public GameObject teamInstance;
    public Color[] colors;*/

    private void Start() // stays here
    {
        //PhotonNetwork.ConnectUsingSettings();

        game.teams = new TeamManager[game.numberOfTeams];

        for (int i = 0; i < game.numberOfTeams; i++)
        {
            GameObject team = Instantiate(game.teamInstance);
            TeamManager manager = team.GetComponent<TeamManager>();
            manager.teamName = "Team " + (i + 1).ToString();
            manager.teamColor = game.colors[i];
            game.teams[i] = manager;
        }

        GameObject player = PhotonNetwork.Instantiate("Demo Player Photon", spawn.transform.position, Quaternion.identity);

        //PhotonNetwork.ConnectUsingSettings(); // probably don't need
    }

    /*public override void OnConnectedToMaster() // in other
    {
        Debug.Log("Connected to master");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() // in other
    {
        Debug.Log("Joined Lobby");

        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = (byte)maxPlayers }, TypedLobby.Default);
    }

    public override void OnJoinedRoom() // in other
    {
        Debug.Log("Joined " + PhotonNetwork.CurrentRoom.Name);

        GameObject player = PhotonNetwork.Instantiate("Demo Player Photon", spawn.transform.position, Quaternion.identity);

        PlayerManager playerInfo = player.GetComponent<PlayerManager>();

        playerInfo.nickname = PhotonNetwork.LocalPlayer.NickName;

        Debug.Log(playerInfo.nickname);
    }*/
}
