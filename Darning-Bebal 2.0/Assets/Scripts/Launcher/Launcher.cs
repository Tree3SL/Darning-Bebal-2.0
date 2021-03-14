using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

    public class Launcher : MonoBehaviourPunCallbacks
    {

        public string roomName;
        public int maxPlayers;

        #region Private Serializable Fields

        [Tooltip("The UI Panel to let user enter name, connect, and play")]
        [SerializeField]
        private GameObject controlPanel;

        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        [SerializeField]
        private string level;

    [SerializeField]
    private Text playerViewportText;

    [SerializeField]
    private GameObject ready;

    [SerializeField]
    private Text team1ViewportText;

    [SerializeField]
    private Text team2ViewportText;

    [SerializeField]
    private GameObject join1;

    [SerializeField]
    private GameObject join2;

    [SerializeField]
    private GameObject nameInput;

    [SerializeField]
    private Color team1Color;

    [SerializeField]
    private Color team2Color;

        #endregion


        #region Private Fields

        private string gameVersion = "1";

        private bool isConnecting;

    int readyPlayers = 0;
    private int teamMaxCount = 0;

        #endregion


        #region MonoBehaviour CallBacks

        void Awake()
        {

            PhotonNetwork.AutomaticallySyncScene = true;

        }


    void Start()
    {
        Connect();
        progressLabel.SetActive(false);
        controlPanel.SetActive(false);
    }

    private void Update()
    { 
        readyPlayers = ready.GetComponent<RpcCalls>().ReadyCheckStatus();
     
        if (PhotonNetwork.InRoom)
        {
            join1.SetActive(CheckTeam("Team1"));
            join2.SetActive(CheckTeam("Team2"));
        }

        if(PhotonNetwork.IsMasterClient && readyPlayers == PhotonNetwork.CurrentRoom.PlayerCount && PhotonNetwork.InRoom)
        {
            controlPanel.transform.Find("Play Button").gameObject.SetActive(true);
        }
        else
        {
            controlPanel.transform.Find("Play Button").gameObject.SetActive(false);
        }

        if(PhotonNetwork.InRoom && readyPlayers > PhotonNetwork.CurrentRoom.PlayerCount && PhotonNetwork.IsMasterClient)
        {
            readyPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            ready.GetComponent<PhotonView>().RPC("ReadyAdjustPlayers", RpcTarget.AllBuffered, readyPlayers);
        }
    }

    #endregion


    #region Public Methods

    public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinLobby();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();

                PhotonNetwork.GameVersion = gameVersion;
            }

        }

    public void Play()
    {
        progressLabel.SetActive(true);
        //controlPanel.SetActive(false);

        PhotonNetwork.CurrentRoom.IsOpen = false;

        PhotonNetwork.LoadLevel(level);
        
    }

    public void Ready()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Team"] != null)
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Ready"] = true;

            playerViewportText.GetComponent<PhotonView>().RPC("UpdateTextReady", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);

            controlPanel.transform.Find("Ready Button").gameObject.SetActive(false);
            nameInput.SetActive(false);

            ready.GetComponent<PhotonView>().RPC("ReadyPushed", RpcTarget.AllBuffered);
        }
    }

    public void JoinTeam(string team)
    { 
        List<string> temp = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties[team];

        if (temp.Count < teamMaxCount)
        {
            if(PhotonNetwork.LocalPlayer.CustomProperties["Team"] != null)
            {
                if(team == "Team1")
                {
                    team2ViewportText.GetComponent<PhotonView>().RPC("UpdateTextRemoveTeamMember", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);
                    photonView.RPC("SyncTeamRemove", RpcTarget.AllBuffered, "Team2", PhotonNetwork.LocalPlayer.NickName);
                }
                else
                {
                    team1ViewportText.GetComponent<PhotonView>().RPC("UpdateTextRemoveTeamMember", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);
                    photonView.RPC("SyncTeamRemove", RpcTarget.AllBuffered, "Team1", PhotonNetwork.LocalPlayer.NickName);
                }
            }

            if (team == "Team1")
            {
                team1ViewportText.GetComponent<PhotonView>().RPC("UpdateTextAddTeamMember", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);
                join1.SetActive(false);
            }
            else
            {
                team2ViewportText.GetComponent<PhotonView>().RPC("UpdateTextAddTeamMember", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);
                join2.SetActive(false);
            }

            photonView.RPC("SyncTeamAdd", RpcTarget.AllBuffered, team, PhotonNetwork.LocalPlayer.NickName);
            PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        }
    }

    public void UpdateNickName(string newName)
    {
        playerViewportText.GetComponent<PhotonView>().RPC("UpdateTextNameChanged", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, newName);

        if((string)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == "Team1")
        {
            team1ViewportText.GetComponent<PhotonView>().RPC("UpdateTextNameChanged", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, newName);
            photonView.RPC("SyncTeamUpdate", RpcTarget.AllBuffered, "Team1", PhotonNetwork.LocalPlayer.NickName, newName);
        }
        else if((string)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == "Team2")
        {
            team2ViewportText.GetComponent<PhotonView>().RPC("UpdateTextNameChanged", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, newName);
            photonView.RPC("SyncTeamUpdate", RpcTarget.AllBuffered, "Team2", PhotonNetwork.LocalPlayer.NickName, newName);
        }
    }

    #endregion


    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master");

            if (isConnecting)
            { 
                PhotonNetwork.JoinLobby();

                isConnecting = false;
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);

            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);

            isConnecting = false;
        }


        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room");

        progressLabel.SetActive(false);
        controlPanel.SetActive(true);

        controlPanel.transform.Find("Play Button").gameObject.SetActive(false);

        PhotonNetwork.LocalPlayer.CustomProperties.Add("Ready", false);
        PhotonNetwork.LocalPlayer.CustomProperties.Add("Team", null);
        PhotonNetwork.LocalPlayer.CustomProperties.Add("Avatar", "Hero1_Female");

        playerViewportText.GetComponent<PhotonView>().RPC("UpdateTextAddedPlayer", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, PhotonNetwork.IsMasterClient);

        teamMaxCount = Mathf.CeilToInt(PhotonNetwork.CurrentRoom.PlayerCount / 2f);

    }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");

        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = (byte)maxPlayers }, TypedLobby.Default);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            string team;
            List<string> temp1 = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties["Team1"];
            List<string> temp2 = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties["Team2"];

            if(temp1.Contains(otherPlayer.NickName))
            {
                team = "Team1";
            }
            else if(temp2.Contains(otherPlayer.NickName))
            {
                team = "Team2";
            }
            else
            {
                team = "";
            }

            if(team == "Team1")
            {
                team1ViewportText.GetComponent<PhotonView>().RPC("UpdateTextRemoveTeamMember", RpcTarget.AllBuffered, otherPlayer.NickName);
            }
            else if(team == "Team2")
            {
                team2ViewportText.GetComponent<PhotonView>().RPC("UpdateTextRemoveTeamMember", RpcTarget.AllBuffered, otherPlayer.NickName);
            }

            if(team != "")
            {
                photonView.RPC("SyncTeamRemove", RpcTarget.AllBuffered, team, otherPlayer.NickName);
            }

            playerViewportText.GetComponent<PhotonView>().RPC("UpdateTextRemovePlayer", RpcTarget.AllBuffered, otherPlayer.NickName, PhotonNetwork.NickName);
        }

        teamMaxCount = Mathf.CeilToInt(PhotonNetwork.CurrentRoom.PlayerCount / 2f);
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        teamMaxCount = Mathf.CeilToInt(PhotonNetwork.CurrentRoom.PlayerCount / 2f);
    }

    public override void OnCreatedRoom()
    {
        photonView.RPC("SyncCustomProperties", RpcTarget.AllBuffered);
    }


    #endregion

    #region Private Methods

    private bool CheckTeam(string team)
    {
        List<string> temp = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties[team];
        
        if(temp.Count < teamMaxCount && !temp.Contains(PhotonNetwork.LocalPlayer.NickName) && !(bool)PhotonNetwork.LocalPlayer.CustomProperties["Ready"])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion


    #region RPC Methods

    [PunRPC]
    public void SyncTeamAdd(string team, string playerName)
    {
        List<string> temp = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties[team];
        temp.Add(playerName);
        PhotonNetwork.CurrentRoom.CustomProperties[team] = temp;
    }

    [PunRPC]
    public void SyncTeamRemove(string team, string playerName)
    {
        List<string> temp = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties[team];
        temp.Remove(playerName);
        PhotonNetwork.CurrentRoom.CustomProperties[team] = temp;
    }

    [PunRPC]
    public void SyncTeamUpdate(string team, string oldNickName, string newNickName)
    {
        List<string> temp = (List<string>)PhotonNetwork.CurrentRoom.CustomProperties[team];
        int index = temp.IndexOf(oldNickName);
        temp[index] = newNickName;
        PhotonNetwork.CurrentRoom.CustomProperties[team] = temp;
    }

    [PunRPC]
    public void SyncCustomProperties()
    {
        PhotonNetwork.CurrentRoom.CustomProperties.Add("Team1", new List<string>());
        PhotonNetwork.CurrentRoom.CustomProperties.Add("Team2", new List<string>());
        PhotonNetwork.CurrentRoom.CustomProperties.Add("Color1", team1Color);
        PhotonNetwork.CurrentRoom.CustomProperties.Add("Color2", team2Color);
    }

    #endregion
}

