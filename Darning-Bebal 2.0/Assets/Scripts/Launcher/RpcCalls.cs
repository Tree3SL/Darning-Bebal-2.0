using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RpcCalls : MonoBehaviour
{
    public Text playerViewportText;

    private int readyPlayers = 0;


    [PunRPC]
    public void UpdateTextAddedPlayer(string nickname, bool isMaster)
    {
        playerViewportText.text += nickname;

        if (isMaster)
        {
            playerViewportText.text += "(Master): waiting...";
        }
        else
        {
            playerViewportText.text += ": waiting...";
        }

        playerViewportText.text += "\n";
    }

    [PunRPC]
    public void UpdateTextReady(string nickname)
    {
        int startIndex = playerViewportText.text.IndexOf(nickname);
        int endIndex = playerViewportText.text.IndexOf("\n", startIndex);
        int length = endIndex - startIndex;

        string substr = playerViewportText.text.Substring(startIndex, length);
        string newsub = substr.Replace("waiting...", "ready");

        playerViewportText.text = playerViewportText.text.Replace(substr, newsub);
    }

    [PunRPC]
    public void UpdateTextRemovePlayer(string nickname, string masterNickName)
    {
        int startIndex = playerViewportText.text.IndexOf(nickname);
        int endIndex = playerViewportText.text.IndexOf("\n", startIndex);
        int length = endIndex - startIndex;

        string substr = playerViewportText.text.Substring(startIndex, length + 1);

        playerViewportText.text = playerViewportText.text.Replace(substr, "");

        int masterCheck = playerViewportText.text.IndexOf(masterNickName + "(Master)");

        if(masterCheck == -1)
        {
            playerViewportText.text = playerViewportText.text.Replace(masterNickName, masterNickName + "(Master)");
        }
    }

    [PunRPC]
    public void UpdateTextNameChanged(string oldNickName, string newNickName)
    {
        playerViewportText.text = playerViewportText.text.Replace(oldNickName, newNickName);
    }

    [PunRPC]
    public void UpdateTextAddTeamMember(string nickname)
    {
        playerViewportText.text += nickname;
        playerViewportText.text += "\n";
    }

    [PunRPC]
    public void UpdateTextRemoveTeamMember(string nickname)
    {
        playerViewportText.text = playerViewportText.text.Replace(nickname + "\n", "");
    }

    [PunRPC]
    public void ReadyPushed()
    {
        readyPlayers++;
    }

    [PunRPC]
    public void ReadyAdjustPlayers(int num)
    {
        readyPlayers = num;
    }

    public int ReadyCheckStatus()
    {
        return readyPlayers;
    }
}
