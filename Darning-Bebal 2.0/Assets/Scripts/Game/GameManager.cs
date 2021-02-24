using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public TeamManager[] teams;

    public int numberOfTeams = 2;
    public GameObject teamInstance;
    public Color[] colors;
}
