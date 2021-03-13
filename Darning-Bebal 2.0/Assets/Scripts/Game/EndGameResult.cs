using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class EndGameResult : MonoBehaviour
{

    public TextMeshProUGUI winner_team_text;
    public TextMeshProUGUI team1_text;
    public TextMeshProUGUI team2_text;
    public Slider team1_slider;
    public Slider team2_slider;

    public TextMeshProUGUI wait_text;
    public GameObject restart_button;

    [SerializeField]
    private string level;

    private void Awake()
    {
        //update value
        float team1_value = team1_slider.value;
        team1_text.text = team1_value.ToString("0");
        float team2_value = team2_slider.value;
        team2_text.text = team2_value.ToString("0");

        //compare and show winner
        if (team1_value > team2_value) 
        {
            winner_team_text.text = "Team 1 Win";
            winner_team_text.color = team1_text.color;
        }
        else if (team1_value < team2_value)
        {
            winner_team_text.text = "Team 2 Win";
            winner_team_text.color = team2_text.color;
        }
        else
        {
            winner_team_text.text = "Tie";
            winner_team_text.color = Color.white;
        }

        //hide wait or restart button
        if (PhotonNetwork.IsMasterClient)
        {
            wait_text.gameObject.SetActive(false);
        }
        else 
        {
            restart_button.SetActive(false);
        }
    }

    //restart function only for master
    public void Restart() 
    {
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.LoadLevel(level);
    }
}
