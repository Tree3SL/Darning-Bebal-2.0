using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorEnum
    {
        Left,
        Right,
    }

    public DoorEnum doorEnum;
    public GameObject endPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DoorEnum GetDoorEnum()
    {
        return doorEnum;
    }

    public GameObject GetObjEnd()
    {
        return endPosition;
    }
}
